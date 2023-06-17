using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : CommonState
{
    float curtime = 0;
    bool curringTime = false;
    private void Start()
    {
        fsm.ChangeState(_myState);
    }

    public override void EnterState()
    {
        curtime = 0;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.OnAnimationEventTrigger += EventAction;

    }

    public override void ExitState()
    {
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }


    public override void UpdateState()
    {
        curtime += Time.deltaTime;
        if(FSMMain.Object.WaitTime > 0 && TimeController.Instance.Timer == 1)
        {
            if (curtime > FSMMain.Object.WaitTime)
            {
                curtime = 0;
                curringTime = true;
                FSMMain.Next();
            }
        }

        //UpdateAction?.Invoke();
    }
}
