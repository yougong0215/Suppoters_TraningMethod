using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUPAction : CommonAction
{
    protected override void OnEndFunc()
    {
        com.FSMMain.ChangeState(FSMState.Idle);
    }

    protected override void OnEventFunc()
    {
    }

    protected override void OnUpdateFunc()
    {
    }
}
