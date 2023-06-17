using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CommonState
{
    
    public override void EnterState()
    {
        _animator.SetMoveAnimation(true);
        _animator.OnAnimationEventTrigger  += EventAction;
        _animator.OnAnimationEndTrigger    += EndAction;
        FSMMain.AG.enabled = true;
        FSMMain.AG.SetDestination(FSMMain.Object.pos);
        FSMMain.Character.enabled = false;
    }

    public override void ExitState()
    {
        _animator.SetMoveAnimation(false);
        _animator.OnAnimationEventTrigger   -= EventAction;
        _animator.OnAnimationEndTrigger     -= EndAction;
        FSMMain.AG.enabled = false;
        FSMMain.Character.enabled = true;
    }



    public override void UpdateState()
    {

        print($"Range : " + FSMMain.AG.remainingDistance);
        if(FSMMain.AG.remainingDistance < 0.2f && !FSMMain.AG.pathPending)
        {
            FSMMain.Next();
        }

        UpdateAction?.Invoke();
    }

}
