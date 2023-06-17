using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GetTypeShape
{
    Box,
    Sphere,
    Polygon
}

public class ClickUI : MonoBehaviour
{
    bool Clicked = false;

    public static bool btnEnable = true;

    public players pl;
    Transform Player; // 플레이어
    Transform center;
    SpriteRenderer shapeSprite; // 형태 스프라이트
    public float radius = 1f; // 형태의 반지름
    public Vector2 size = Vector2.one; // 형태의 크기 (너비와 높이)

    SkillUIList gm;
    Canvas cans;
    Vector3 vec;
    Button bt;
    Collider colliders;
    [SerializeField] GetTypeShape tp;

    [SerializeField] skillinfo info;


    public T FindComponentInChildren<T>(GameObject parentObject) where T : Component
    {
        T[] components = parentObject.GetComponentsInChildren<T>(true);

        foreach (T component in components)
        {
            if (component.gameObject.activeSelf)
            {
                return component;
            }
        }

        return null;
    }

    public void onClick()
    {
        gm = GameObject.Find("List").GetComponent<SkillUIList>();
        if (SkillUIList.count > gm.ReturnCount())
        {
            Clicked = true;
            center.gameObject.SetActive(true);
            vec = gm.returnpos();
            cans.planeDistance = 100;
            btnEnable = false;
        }
    }

    private void Awake()
    {
        Player = GameObject.Find(pl.ToString()).transform;
        cans = GameObject.Find("Render").GetComponent<Canvas>();
        print(Player.name);

        switch (tp)
        {
            case GetTypeShape.Sphere:
                center = Player.Find("Sphere").transform;
                shapeSprite = center.GetComponent<SpriteRenderer>();
                break;
            case GetTypeShape.Box:
                center = Player.Find("Box").transform;
                shapeSprite = center.GetChild(0).GetComponent<SpriteRenderer>();
                break;
            case GetTypeShape.Polygon:
                center = Player.Find("Polygon").transform;
                shapeSprite = center.GetChild(0).GetComponent<SpriteRenderer>();
                break;
        };
        Debug.Log(shapeSprite.name);
        colliders = shapeSprite.GetComponent<Collider>();
        radius = GetShapeRadius();
        center.gameObject.SetActive(false);
        bt = GetComponent<Button>();
    }

    private float GetShapeRadius()
    {
        if (shapeSprite == null)
            return 0f;

        Debug.Log(colliders);

        if (colliders == null)
        {
            Debug.LogWarning("콜라이더가 없는 형태 유형입니다.");
            return 0f;
        }

        if (colliders is SphereCollider sphereCollider)
        {
            return sphereCollider.radius;
        }
        else if (colliders is BoxCollider boxCollider)
        {
            return Mathf.Max(boxCollider.size.x, boxCollider.size.y, boxCollider.size.z) / 2f;
        }
        else if (colliders is MeshCollider meshCollider)
        {
            Bounds bounds = meshCollider.bounds;
            return Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) / 2f;
        }
        else
        {
            Debug.LogWarning("지원되지 않는 형태 유형입니다.");
            return 0f;
        }
    }

    private void Update()
    {
        if (Clicked == true)
        {
            if (vec == Vector3.zero)
            {
                center.position = Player.position;
            }
            else
            {
                center.position = vec;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 클릭한 위치의 스크린 좌표를 가져옴
                Vector3 screenPos = Input.mousePosition;

                // 클릭한 위치의 3D 좌표를 계산
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 clickPos = hit.point;
                    float distance = GetDistanceToShape(clickPos);
                    Debug.Log($"{distance}, {GetShapeRadius()}");
                    // 클릭한 위치가 형태의 범위 안에 있는지 확인
                    if (distance*0.1f <= GetShapeRadius())
                    {
                        clickPos.y = 0;
                        skillinfo skilled = info;
                        gm.Setting(skilled, clickPos, Player);
                        // 클릭한 위치의 3D 좌표를 디버그 로그로 출력
                        Debug.Log("클릭한 위치: " + clickPos);
                        Clicked = false;
                    }
                    else
                    {
                        // 클릭한 위치가 형태의 범위 밖에 있음
                        Clicked = false;
                        Debug.Log("클릭한 위치: " + clickPos);
                    }
                }
                else
                {
                    Clicked = false;
                }
                center.gameObject.SetActive(false);
                cans.planeDistance = 1;
                btnEnable = true;
            }
        }

        bt.enabled = btnEnable;
    }

    private float GetDistanceToShape(Vector3 point)
    {
        if (shapeSprite == null)
            return 0f;

        if (colliders == null)
        {
            Debug.LogWarning("콜라이더가 없는 형태 유형입니다.");
            return 0f;
        }

        if (colliders is SphereCollider)
        {
            return Vector3.Distance(point, center.position);
        }
        else if (colliders is BoxCollider boxCollider)
        {
            return DistanceToRectangle(point, boxCollider);
        }
        else if (colliders is MeshCollider meshCollider)
        {
            return DistanceToMesh(point, meshCollider);
        }
        else
        {
            Debug.LogWarning("지원되지 않는 형태 유형입니다.");
            return 0f;
        }
    }

    private float DistanceToRectangle(Vector3 point, BoxCollider boxCollider)
    {
        Vector3 localPoint = center.InverseTransformPoint(point);
        Vector3 size = boxCollider.size;
        Vector3 scaledSize = new Vector3(size.x * center.lossyScale.x, size.y * center.lossyScale.y, size.z * center.lossyScale.z);
        Vector3 halfSize = scaledSize / 2f;
        Vector3 min = -halfSize;
        Vector3 max = halfSize;

        float dx = Mathf.Max(min.x - localPoint.x, 0, localPoint.x - max.x);
        float dy = Mathf.Max(min.y - localPoint.y, 0, localPoint.y - max.y);
        float dz = Mathf.Max(min.z - localPoint.z, 0, localPoint.z - max.z);

        return Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    private float DistanceToMesh(Vector3 point, MeshCollider meshCollider)
    {
        Vector3 localPoint = center.InverseTransformPoint(point);
        Mesh mesh = meshCollider.sharedMesh;
        float closestDistance = float.MaxValue;

        if (mesh != null)
        {
            foreach (var triangle in mesh.triangles)
            {
                Vector3 vertex = mesh.vertices[triangle];
                Vector3 worldVertex = center.TransformPoint(vertex);
                float distance = Vector3.Distance(localPoint, worldVertex);
                closestDistance = Mathf.Min(closestDistance, distance);
            }
        }

        return closestDistance;
    }
}
