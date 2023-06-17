using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour
{
    bool Clicked = false;

    public static bool btnEnable = true;

    public players pl;
    Transform Player; // 플레이어
    Transform center;
    SpriteRenderer circleSprite; // Circle Sprite
    public float radius = 1f; // 원의 반지름

    SkillUIList gm;
    Canvas cans;
    Vector3 vec;
    Button bt;

    [SerializeField] skillinfo info;
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
        center = Player.Find("center").transform;
        circleSprite = center.GetComponent<SpriteRenderer>();
        radius = circleSprite.bounds.size.x / 2f;
        center.gameObject.SetActive(false);
        bt = GetComponent<Button>();
    }
    private void Update()
    {
        if(Clicked == true)
        {

            if(vec == Vector3.zero)
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
                    float distance = Vector2.Distance(clickPos, center.position);
                    clickPos.y = 1;


                    // 클릭한 위치가 원의 범위 안에 있는지 확인
                    if (distance <= circleSprite.bounds.size.x / 2f)
                    {
                        skillinfo skilled = info;
                        gm.Setting(skilled, clickPos, Player);
                        // 클릭한 위치의 3D 좌표를 디버그 로그로 출력
                        Debug.Log("Clicked Position: " + clickPos);
                        Clicked = false;
                    }
                    else
                    {
                        // 클릭한 위치가 원의 범위 밖에 있음
                        Clicked = false;
                        Debug.Log("Clicked Position: " + clickPos);
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
}
