using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFalse : MonoBehaviour
{
    public players pl;

    FSM fm;
    private void Awake()
    {
        fm = GameObject.Find(pl.ToString()).GetComponent<FSM>();
    }

    private void Update()
    {
        if(fm.NowState()== FSMState.Death)
        {
            gameObject.SetActive(false);
        }
    }
}
