using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : CommonState
{
    // Start is called before the first frame update
    

    private void Start()
    {
        fsm.ChangeState(_myState);
    }

    public override void EnterState()
    {
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.OnAnimationEventTrigger += EventAction;
        FSMMain.Character.enabled = true;
        FSMMain.AG.enabled = false;
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

