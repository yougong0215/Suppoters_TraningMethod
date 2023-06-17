using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour
{
    bool Clicked = false;

    public static bool btnEnable = true;

    public players pl;
    Transform Player; // �÷��̾�
    Transform center;
    SpriteRenderer circleSprite; // Circle Sprite
    public float radius = 1f; // ���� ������

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
                // ���콺 Ŭ���� ��ġ�� ��ũ�� ��ǥ�� ������
                Vector3 screenPos = Input.mousePosition;

                // Ŭ���� ��ġ�� 3D ��ǥ�� ���
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 clickPos = hit.point;
                    float distance = Vector2.Distance(clickPos, center.position);
                    clickPos.y = 1;


                    // Ŭ���� ��ġ�� ���� ���� �ȿ� �ִ��� Ȯ��
                    if (distance <= circleSprite.bounds.size.x / 2f)
                    {
                        skillinfo skilled = info;
                        gm.Setting(skilled, clickPos, Player);
                        // Ŭ���� ��ġ�� 3D ��ǥ�� ����� �α׷� ���
                        Debug.Log("Clicked Position: " + clickPos);
                        Clicked = false;
                    }
                    else
                    {
                        // Ŭ���� ��ġ�� ���� ���� �ۿ� ����
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
