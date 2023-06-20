using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CommonState
{
    float MoveTime = 0;
    float curtime = -0.5f;
    Vector3 vec;
    
    public override void EnterState()
    {
        _animator.SetMoveAnimation(true);
        _animator.OnAnimationEventTrigger  += EventAction;
        _animator.OnAnimationEndTrigger    += EndAction;
        FSMMain.AG.enabled = true;
        FSMMain.Character.enabled = false;
        MoveTime = 0;
        curtime = -0.5f;
        if (FSMMain.gameObject.name == "Boss")
        {
            vec = GenerateRandomPosition();
            FSMMain.LookRotations(vec);
        }
        else
            vec = FSMMain.Object.pos;

    }

    public override void ExitState()
    {
        _animator.SetMoveAnimation(false);
        _animator.OnAnimationEventTrigger   -= EventAction;
        _animator.OnAnimationEndTrigger     -= EndAction;
        FSMMain.AG.enabled = false;
        FSMMain.Character.enabled = true;
    }


    Vector3 GenerateRandomPosition()
    {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized * 5;
        return FSMMain.SeeEnemy + new Vector3(randomCircle.x, 0f, randomCircle.y);
    }

    public override void UpdateState()
    {
        MoveTime += Time.deltaTime;
        curtime += Time.deltaTime;
        FSMMain.AG.SetDestination(vec);
        curtime = Mathf.Clamp(curtime, -0.5f, 0.3f);
        //print($"Range : " + FSMMain.AG.remainingDistance);
        if ((FSMMain.AG.remainingDistance < 0.2f) && MoveTime > 0.2f)
        {
            if(FSMMain.gameObject.name=="Boss")
            {
                FSMMain.LookRotations(FSMMain.ts.position);
                FSMMain.ChangeState(FSMState.Skill);
            }
            else
                FSMMain.ChangeState(FSMState.Idle);
        }

        UpdateAction?.Invoke();
    }

}
