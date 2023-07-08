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
        {
            if(FSMMain.Object.InPos==true)
            {

                vec = FSMMain.Object.dir;
            }
            else
            {
                FSMMain.SeeEnemy = FSMMain.ts.position;
                vec = GenerateRandomPosition2();
            }
        }

    }

    public override void ExitState()
    {
        _animator.SetMoveAnimation(false);
        _animator.OnAnimationEventTrigger   -= EventAction;
        _animator.OnAnimationEndTrigger     -= EndAction;
        FSMMain.AG.enabled = false;
        FSMMain.Character.enabled = true;
    }


    Vector3 GenerateRandomPosition() { 
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized * 3.5f;

        return FSMMain.SeeEnemy + new Vector3(randomCircle.x, 0f, randomCircle.y);

    }

    Vector3 GenerateRandomPosition2()
    {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized * 3.5f;

        return new Vector3(randomCircle.x, 0f, randomCircle.y);

    }

    public override void UpdateState()
    {
        MoveTime += Time.deltaTime;
        curtime += Time.deltaTime;
        if (FSMMain.gameObject.name == "Boss")
        {
            FSMMain.AG.SetDestination(vec);
            if ((FSMMain.AG.remainingDistance < FSMMain.ststed.stat._distance + 0.05f) && MoveTime > 0.2f)
            {
                FSMMain.LookRotations(FSMMain.ts.position);
                FSMMain.ChangeState(FSMState.Skill);
            }
        }
        else
        {
            if(FSMMain.AG.enabled == true)
            if (FSMMain.Object.InPos == false)
            {
                    if (fsm.AutoMove == false)
                    {
                        FSMMain.ChangeState(FSMState.Idle);
                        return;
                    }

                    FSMMain.SeeEnemy = FSMMain.ts.position;
                    FSMMain.LookRotations(vec + FSMMain.SeeEnemy);
                FSMMain.AG.SetDestination(vec + FSMMain.SeeEnemy);
                if ((FSMMain.AG.remainingDistance < FSMMain.ststed.stat._distance + 0.05f) && MoveTime > 0.2f)
                {
                    FSMMain.ChangeState(FSMState.Idle);
                }



            }
            else
            {
                FSMMain.AG.SetDestination(vec);
                    FSMMain.LookRotations(vec);
                    if ((FSMMain.AG.remainingDistance < 0.05f ) && MoveTime > 0.2f)
                {
                    FSMMain.ChangeState(FSMState.Idle);
                }
            }

        }
        
        curtime = Mathf.Clamp(curtime, -0.5f, 0.3f);
        //print($"Range : " + FSMMain.AG.remainingDistance);

        UpdateAction?.Invoke();
    }

}
