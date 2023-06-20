using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
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
    Vector3 vec;
    Button bt;
    Collider colliders;
    [SerializeField] GetTypeShape tp;
    CinemachineVirtualCamera cans;
    [SerializeField] skillinfo info;
    [SerializeField] Vector3 Scale;


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

        cans.Priority = 100;
        if (info.state == FSMState.Idle && SkillUIList.count > gm.ReturnCount())
        {
            skillinfo skilled = info;
            Debug.Log(info.state);
            gm.Setting(skilled, Player.transform.position, Player);
            return;
        }

        if (info.state != FSMState.Idle && SkillUIList.count > gm.ReturnCount() && gm.Cost >= info.Cost)
        {
            Clicked = true;
            cans.Priority = 100;
            center.gameObject.SetActive(true);
            center.localScale = Scale;
            vec = gm.returnpos();
            btnEnable = false;
        }
        GameManager.Instance.Cam.depth = 1;//GetComponent<UniversalAdditionalCameraData>().allow
    }

    private void Awake()
    {
        Player = GameObject.Find(pl.ToString()).transform;
        //print(Player.name);
        cans = GameObject.Find($"{pl.ToString()}TopCam").GetComponent<CinemachineVirtualCamera>();
        foreach (TextMeshProUGUI tmp in transform.GetComponentsInChildren<TextMeshProUGUI>())
        {
            tmp.raycastTarget = false;
        }
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
        info.spi = GetComponent<Image>().sprite;
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
    bool IsPositionOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, 0.1f, NavMesh.AllAreas);
    }
    private void Update()
    {
        if (Clicked == true)
        {
            GameManager.Instance.Cam.depth = 4;
            if (vec == Vector3.zero)
            {
                center.position = Player.position;

            }
            else
            {
                center.position = vec;
            }
            center.position += new Vector3(0, 0.2f, 0);
            cans.transform.position = center.position + new Vector3(0, 30, 0);

            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 클릭한 위치의 스크린 좌표를 가져옴
                Vector3 screenPos = Input.mousePosition;

                // 클릭한 위치의 3D 좌표를 계산
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit, 1 << 10))
                {
                    Vector3 clickPos = hit.point;
                    float distance = GetDistanceToShape(clickPos);
                    Debug.Log($"DIS : {distance}, {GetShapeRadius()}");
                    // 클릭한 위치가 형태의 범위 안에 있는지 확인
                    clickPos.y = 0;
                    if (distance* 0.1f <= GetShapeRadius())
                    {
                        if(IsPositionOnNavMesh(clickPos) == true)
                        {
                            skillinfo skilled = info;
                            skilled.dir = clickPos;
                            Debug.Log(info.state);
                            gm.Setting(skilled, clickPos, Player);
                            // 클릭한 위치의 3D 좌표를 디버그 로그로 출력
                            Debug.Log("클릭한 위치: " + clickPos);
                            Clicked = false;
                        }
                        else
                        {
                            Debug.Log("클릭한 위치: " + clickPos);
                            Clicked = false;
                            GameController.Contorller.TMPSetMessage("No Ground");
                        }


                    }
                    else
                    {
                        // 클릭한 위치가 형태의 범위 밖에 있음
                        Clicked = false;
                        Debug.Log("클릭한 위치: " + clickPos);
                        GameController.Contorller.TMPSetMessage("Out Range");
                    }
                }
                else
                {
                    Clicked = false;
                    GameController.Contorller.TMPSetMessage("No Range");
                }
                center.gameObject.SetActive(false);
                GameManager.Instance.Cam.depth = 1;
                cans.Priority = 1;
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
