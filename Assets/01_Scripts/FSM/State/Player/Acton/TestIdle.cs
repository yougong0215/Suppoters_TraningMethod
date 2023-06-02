using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIdle : CommonAction
{
    protected override void OnEndFunc()
    {
    }

    protected override void OnEventFunc()
    {
    }

    protected override void OnUpdateFunc()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            com.FSMMain.ChangeState(FSMState.Move);
            Debug.Log("Change");
        }
        if(Input.GetMouseButton(0))
        {
            com.FSMMain.ChangeState(FSMState.Skill);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            com.FSMMain.ChangeState(FSMState.Hit);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            com.FSMMain.ChangeState(FSMState.nuckdown);
        }
    }


}
