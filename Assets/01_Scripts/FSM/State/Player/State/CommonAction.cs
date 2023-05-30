using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonAction : MonoBehaviour
{
    protected CommonState com;

    protected virtual void OnEnable()
    {
        com = transform.parent.GetComponent<CommonState>();


        com.EventAction += OnEventFunc;
        com.EndAction += OnEndFunc;
        com.UpdateAction += OnUpdateFunc;
    }


    protected abstract void OnEventFunc();
    protected abstract void OnEndFunc();
    protected abstract void OnUpdateFunc();
}
