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

    bool click = false;
    private void Awake()
    {
        game = transform.parent.parent.GetComponent<InGameCharacterBar>();
        if(_charSpi != null)
            CharacterImage.sprite = _charSpi;

        CostUI.text = explain;
        ExplainUI.text = $"Cost : {Cost}";
    }

    public void OnClick()
    {
        click = true;
        transform.parent.parent.parent.GetComponent<RectTransform>().position -= new Vector3(0, 400, 0);
    }

    private void Update()
    {
        if(click==true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                click = false;
                transform.parent.parent.parent.GetComponent<RectTransform>().position += new Vector3(0, 400, 0);
                Vector3 screenPos = Input.mousePosition;

                // 클릭한 위치의 3D 좌표를 계산
                Ray ray = GameManager.Instance.Cam.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit hit, 1 << 10))
                {
                    skill.dir = hit.point;
                    game._info.GetComponent<FSM>().Next(skill);
                    skill.dir = new Vector3(0, 0, 0);

                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                click = false;
                transform.parent.parent.parent.GetComponent<RectTransform>().position += new Vector3(0, 400, 0);
            }

        }
    }
}
