using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUPAction : CommonAction
{
    bool wakeup = false;
    protected override void Init()
    {

    }

    protected override void OnEndFunc()
    {
        wakeup =true;
    }

    protected override void OnEventFunc()
    {
    }

    protected override void OnUpdateFunc()
    {
        if(com.FSMMain.NowState() == FSMState.WakeUp && wakeup ==true)
        {
            com.FSMMain.ChangeState(FSMState.Idle);
        }
    }
}
