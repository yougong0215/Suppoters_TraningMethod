using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSecond : MonoBehaviour
{
    [SerializeField] int count;
    [SerializeField] SkillUIList ls;

    public void Click()
    {
        try
        {
            ls = GameObject.FindGameObjectWithTag("List").GetComponent<SkillUIList>();
            ls.ClickSecond(count);
        }
        catch
        {

        }

    }
}
