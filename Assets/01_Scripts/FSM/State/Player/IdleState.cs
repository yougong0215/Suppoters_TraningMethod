using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CommonState
{
    private void Start()
    {
        fsm.ChangeState(_myState);
    }

    public override void EnterState()
    {

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
        UpdateAction?.Invoke();
    }
}
