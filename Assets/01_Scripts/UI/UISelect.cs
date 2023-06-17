using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : MonoBehaviour
{
    public players PL;
    public CharUIController con;
    Button btn;
    public SkillUIList ls;

    public TextMeshProUGUI tmp;
    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    public void Click()
    {
        con.Click(PL);
        Debug.Log("Å¬¸¯");
    }
    private void Update()
    {
        btn.enabled = ClickUI.btnEnable;
        tmp.text = $"{PL.ToString()} : [ {ls.ReturnCount()} / {SkillUIList.count} ] ";
    }
}

