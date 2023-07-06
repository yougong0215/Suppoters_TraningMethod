using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIPanel : MonoBehaviour
{
    public static bool Selected = false;

    [SerializeField] LayerMask layer;
    InGameCharacterBar game;
    [Header("Import")]
    [SerializeField] Sprite _charSpi;
    [SerializeField] Image CharacterImage;
    [SerializeField] TextMeshProUGUI CostUI;
    [SerializeField] TextMeshProUGUI SkillName;
    [SerializeField] TextMeshProUGUI ExplainUI;

    [Header("Explain")]
    [SerializeField] int Cost;
    [SerializeField] string names;
    [SerializeField] [TextArea] string explain;


    [Header("Skill")]
    [SerializeField] skillinfo skill;

    float _originPosX;
    RectTransform me;
    RectTransform parent;
    public bool click { get; private set; } = false;

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
        SkillName.text = names;
    }

    public void OnClick()
    {
        if(CanSelect())
        {
            if(fs.Object.useSkill == false)
            {
                if(game.Cost >= Cost)
                {

                    Selected = true;
                    click = true;
                    parent.position -= new Vector3(0, 200, 0);
                    me.localPosition = new Vector3(-140, 1120, 0);
                    GetComponent<RectTransform>();
                    tp.SelectStop();
                    
                }
                else
                {
                    TextDrowUI t = PoolManager.Instance.Pop("DrawText") as TextDrowUI;

                    t.Init("ĳ������ �ڽ�Ʈ�� �����մϴ�.", 1);
                }

            }
            else
            {
                TextDrowUI t = PoolManager.Instance.Pop("DrawText") as TextDrowUI;

                t.Init("ĳ���Ͱ� �ൿ �� �Դϴ�.", 1);
            }

        }
        else
        {
            TextDrowUI t = PoolManager.Instance.Pop("DrawText") as TextDrowUI;

            t.Init("ĳ���Ͱ� �ൿ �Ұ� ���� �Դϴ�.",1);
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

                parent.position += new Vector3(0, 200, 0);

                me.localPosition = new Vector3(_originPosX, 0, 0);

                Vector3 screenPos = Input.mousePosition;

                // Ŭ���� ��ġ�� 3D ��ǥ�� ���
                Ray ray = GameManager.Instance.Cam.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit, 200, layer))
                {
                    click = false;

                    game.Cost -= Cost;
                    skill.dir = hit.point;
                    game._info.GetComponent<FSM>().Next(skill);
                    skill.dir = new Vector3(0, 0, 0);
                    ClickPosCheckLine ck = PoolManager.Instance.Pop("ClickPos") as ClickPosCheckLine;

                    ck.Init(hit.point);
                    Selected = false;
                    tp.SelectEnd();
                    game.OnClick();
                }

            }
            else if (Input.GetMouseButtonDown(1))
            {
                click = false;
                parent.position += new Vector3(0, 200, 0);
                me.localPosition = new Vector3(_originPosX, 0, 0);
                tp.SelectEnd();
                Selected = false;
                game.OnClick();
            }

            

        }
        
    }
}
