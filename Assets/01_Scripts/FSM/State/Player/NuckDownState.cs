using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuckDownState : CommonState
{
    public override void EnterState()
    {
        _animator.SetNuckDownAnimation(true);
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
    }


    public override void ExitState()
    {
        _animator.SetNuckDownAnimation(false);
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }


    public override void UpdateState()
    {
        UpdateAction?.Invoke();
    }
}
