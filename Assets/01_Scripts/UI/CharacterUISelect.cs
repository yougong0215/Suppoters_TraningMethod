using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUISelect : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] List<InGameCharacterBar> _list = new();
    [SerializeField] InGameCharacterBar SelectUI;
    [Header("Position")]
    [SerializeField] RectTransform View;
    [SerializeField] RectTransform Scroll;

    [SerializeField] skillinfo skill;
    private void Awake()
    {
        foreach (InGameCharacterBar ch in _list)
        {
            if (ch.Select == true)
            {
                SelectUI = ch;
                SelectUI.transform.parent = View.transform;
                SelectUI.GetComponent<RectTransform>().position = View.position;
                ch.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                ch.transform.parent = Scroll;
                ch.transform.localScale = new Vector3(1, 1, 1) * 0.5f;
            }
        }
    }
    private void Update()
    {


        if(SelectUI.Select == false)
        {
            foreach(InGameCharacterBar ch in _list)
            {
                if(ch.Select==true)
                {
                    SelectUI = ch;
                    SelectUI.transform.parent = View.transform;
                    SelectUI.GetComponent<RectTransform>().position = View.position;
                    ch.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    ch.transform.parent = Scroll;
                    ch.transform.localScale = new Vector3(1, 1, 1) * 0.5f;
                }
            }
        }


        if(SkillUIPanel.Selected == false && Input.GetMouseButtonDown(1) && SelectUI._info.GetComponent<FSM>().CanSelect())
        {
            Vector3 screenPos = Input.mousePosition;
            Ray ray = GameManager.Instance.Cam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 200, layer))
            {
                skill.dir = hit.point;
                SelectUI._info.GetComponent<FSM>().Next(skill);
                skill.dir = new Vector3(0, 0, 0);
                ClickPosCheckLine ck = PoolManager.Instance.Pop("ClickPos") as ClickPosCheckLine;

                ck.Init(hit.point);
            }
        }

    }

    public void Select()
    {
        foreach (InGameCharacterBar ch in _list)
        {
            ch.Select = false;
        }
    }

    public void ObjectActive(bool b)
    {

        Scroll.gameObject.SetActive(b);

    }



}
