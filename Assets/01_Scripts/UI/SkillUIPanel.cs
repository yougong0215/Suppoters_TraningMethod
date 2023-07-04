using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIPanel : MonoBehaviour
{

    InGameCharacterBar game;
    [Header("Import")]
    [SerializeField] Sprite _charSpi;
    [SerializeField] Image CharacterImage;
    [SerializeField] TextMeshProUGUI CostUI;
    [SerializeField] TextMeshProUGUI ExplainUI;

    [Header("Explain")]
    [SerializeField] int Cost;
    [SerializeField] [TextArea] string explain;


    [Header("Skill")]
    [SerializeField] skillinfo skill;

    float _originPosX;
    RectTransform me;
    RectTransform parent;
    bool click = false;

    TimeStopButton tp;

    FSM fs;
    private void Awake()
    {
        game = transform.parent.parent.GetComponent<InGameCharacterBar>();
        fs = game._info.GetComponent<FSM>();
        if (_charSpi != null)
            CharacterImage.sprite = _charSpi;

        CostUI.text = explain;
        ExplainUI.text = $"Cost : {Cost}";
        
        tp = GameObject.Find("TimeStop").GetComponent<TimeStopButton>();
        me = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();
        _originPosX = me.localPosition.x;
    }

    public void OnClick()
    {
        if(CanSelect() && fs.Object.useSkill==false)
        {
            click = true;
            parent.position -= new Vector3(0, 200, 0);
            me.localPosition = new Vector3(-140, 1120, 0);
            GetComponent<RectTransform>();
            tp.SelectStop();
        }
    }

    bool CanSelect()
    {
        if(fs.NowState() != FSMState.nuckdown && fs.NowState() != FSMState.WakeUp && fs.NowState() != FSMState.Death)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(CanSelect() == false && click)
        {

            click = false;
            parent.position += new Vector3(0, 200, 0);

            me.localPosition = new Vector3(_originPosX, 0, 0);

            tp.SelectEnd();
        }

        if(click)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                click = false;
                parent.position += new Vector3(0, 200, 0);

                me.localPosition = new Vector3(_originPosX, 0, 0);

                Vector3 screenPos = Input.mousePosition;

                // 클릭한 위치의 3D 좌표를 계산
                Ray ray = GameManager.Instance.Cam.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit, 1 << 10))
                {
                    skill.dir = hit.point;
                    game._info.GetComponent<FSM>().Next(skill);
                    skill.dir = new Vector3(0, 0, 0);

                }
                tp.SelectEnd();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                click = false;
                parent.position += new Vector3(0, 200, 0);
                me.localPosition = new Vector3(_originPosX, 0, 0);
                tp.SelectEnd();
            }

            

        }
        
    }
}
