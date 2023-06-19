using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CommonState
{
    float MoveTime = 0;
    
    public override void EnterState()
    {
        _animator.SetMoveAnimation(true);
        _animator.OnAnimationEventTrigger  += EventAction;
        _animator.OnAnimationEndTrigger    += EndAction;
        FSMMain.AG.enabled = true;
        FSMMain.Character.enabled = false;
        MoveTime = 0;
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
        MoveTime += Time.deltaTime;

        FSMMain.AG.SetDestination(FSMMain.Object.pos);
        //print($"Range : " + FSMMain.AG.remainingDistance);
        if ((FSMMain.AG.remainingDistance < 0.2f) && MoveTime > 0.2f)
        {
            FSMMain.ChangeState(FSMState.Idle);
        }

        UpdateAction?.Invoke();
    }

}
