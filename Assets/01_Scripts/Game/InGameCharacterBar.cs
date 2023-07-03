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

        SkillList.SetActive(false);
    }


    public void OnClick()
    {

        if(_select == false)
        {
            ui.Select();
            _select = true;
            click = true;

            ui.ObjectActive(true);
            SkillList.SetActive(false);
            
            CameraController.Instance.SetCam(_info.pl);
            
            // ø©±‚º≠ πŸ≤„¡‡æﬂµ 
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

            // UI ∂Áøˆ¡÷±‚
        }
    }

    private void Update()
    {
        HPBar.fillAmount = Mathf.Lerp(0, 1, (float)_info.HP / (float)_info.MaxHP);
        HPText.text = $"{_info.HP}/{_info.MaxHP}";
    }
}
