using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InGameCharacterBar : MonoBehaviour
{
    [SerializeField] public AgentStatus _info;

    [Header("Image")]
    [SerializeField] Sprite _charSpi;
    [SerializeField] Image CharacterImage;

    [Header("HP")]
    [SerializeField] Image HPBar;
    [SerializeField] TextMeshProUGUI HPText;

    [Header("MP")]
    [SerializeField] Image MPBar;
    [SerializeField] TextMeshProUGUI MPText;

    [Header("Info")]
    [SerializeField] bool _select = false;
    [SerializeField] GameObject SkillList;
    [SerializeField] public int Cost;
    [SerializeField] TextMeshProUGUI Sibal;
    [SerializeField] string CharName;

    [Header("AutoMove")]
    [SerializeField] Image checkImg;
    [SerializeField] Sprite Check;
    [SerializeField] Sprite nonCehck;

    public bool Lobby = false;
    
    public void OnAutmoveClick()
    {
        if(_info.GetComponent<FSM>().AutoMove == false)
        {
            checkImg.sprite = Check;
            _info.GetComponent<FSM>().AutoMove = true;
        }
        else
        {
            _info.GetComponent<FSM>().AutoMove = false;
            checkImg.sprite = nonCehck;
        }
    }

    bool click = true;
    CharacterUISelect ui;
    public bool Select
    {
        get
        {
            return _select;
        }
        set
        {
            _select = value;
        }
    }
    private void Awake()
    {
        ui = GameObject.Find("CharUIS").GetComponent<CharacterUISelect>();
        if (_charSpi != null)
            CharacterImage.sprite = _charSpi;

        Cost = _info.stat.Cost;
        SkillList.SetActive(false);
        Sibal.text = CharName;

    }

    private void Start()
    {
        if (_select)
        {
            CameraController.Instance.SetCam(_info.pl);
        }
    }

    public void OnClick()
    {
        if (Lobby == true)
            return;
        if(_select == false)
        {
            ui.Select();
            _select = true;
            click = true;

            ui.ObjectActive(true);
            SkillList.SetActive(false);
            
            CameraController.Instance.SetCam(_info.pl);
            
            // ¿©±â¼­ ¹Ù²ãÁà¾ßµÊ
        }
        else
        {
            if(click==false)
            {
                click = true;
                ui.ObjectActive(true);
                SkillList.SetActive(false);
            }
            else
            {
                click = false;
                ui.ObjectActive(false);
                SkillList.SetActive(true);
            }

            // UI ¶ç¿öÁÖ±â
        }
    }

    float curtime = 0;

    private void Update()
    {
        if (Lobby == true)
            return;
        if (curtime >= 1 && Cost < _info.stat.Cost)
        {
            Cost += 1;
            curtime = 0;
        }
        else
        {
            curtime += Time.deltaTime;
        }
        HPBar.fillAmount = Mathf.Lerp(0, 1, (float)_info.HP / (float)_info.MaxHP);
        HPText.text = $"{_info.HP}/{_info.MaxHP}";
        MPBar.fillAmount = Mathf.Lerp(0, 1, (float)Cost / (float)_info.stat.Cost);
        MPText.text = $"{Cost}/{ _info.stat.Cost}"; 
        Cost = Mathf.Clamp(Cost, 0, _info.stat.Cost);
        _info.Cost = Cost;
    }
}
