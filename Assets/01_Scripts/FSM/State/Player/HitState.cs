using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CommonState
{
    private void Start()
    {
        EndAction += EndAnimation;
    }
    public override void EnterState()
    {
        _animator.SetHitAnimation(true);



        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
        
    }

    public override void ExitState()
    {
        _animator.SetHitAnimation(false);
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }

    void EndAnimation()
    {
        fsm.ChangeState(FSMState.Idle);
    }

    public override void UpdateState()
    {

    }

}
