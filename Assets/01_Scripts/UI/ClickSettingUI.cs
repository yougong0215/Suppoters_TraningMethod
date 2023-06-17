using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSettingUI : MonoBehaviour
{
    SkillUIList gm;

    int n = 0;
    bool b = false;
    public void bSet()
    {
        b = true;
    }
    public void SetNums(int n)
    {
        this.n = n;
    }
    public void onClick()
    {
        Debug.Log(n);
        if (b == true)
        {

            gm = GameObject.Find("List").GetComponent<SkillUIList>();
           gm.RemoveNum(n);
        }
    }
}
