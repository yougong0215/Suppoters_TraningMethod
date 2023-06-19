using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : CommonState
{ 
    // Start is called before the first frame update

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

