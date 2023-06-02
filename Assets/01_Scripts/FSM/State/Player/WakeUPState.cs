using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUPState : CommonState
{
    public override void EnterState()
    {
        _animator.SetWakeAnimation(true);
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
    }


    public override void ExitState()
    {
        _animator.SetWakeAnimation(false);
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }

    public override void UpdateState()
    {
        UpdateAction?.Invoke();
    }

}
