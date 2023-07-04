using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterIdleState : CommonState
{
    public float curtime = 0;
    public bool curringTime = false;

    public float DIeTime;

    private void Start()
    {
        fsm.ChangeState(_myState);
    }

    public override void EnterState()
    {
        

        DIeTime = 0;
        curtime = 0;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.OnAnimationEventTrigger += EventAction;
        FSMMain.Character.enabled = true;
        FSMMain.AG.enabled = false;




        //if (_skillSO != null && _skillSO.dashAgain == true)
        //{
        //    PoolAble obj = PoolManager.Instance.Pop(_skillSO.SkillObj.name);

        //    if(FSMMain.Object.InPos)
        //    {
        //        obj.transform.position = FSMMain.transform.position;
        //    }
        //    else
        //    {

        //        obj.transform.position = FSMMain.Object.dir;
        //    }

        //    if (obj.GetComponent<DamageCaster>())
        //    {

        //        obj.GetComponent<DamageCaster>().Init((int)((FSMMain.ststed.stat.ATK + FSMMain.ststed.AddDamage) * FSMMain.ststed.himsDamage), FSMMain.ststed.stat.Critical + FSMMain.ststed.Cirt, FSMMain.ststed.stat.CriticalDamage + FSMMain.ststed.CirtDAM);
        //    }
        //    obj.transform.rotation = Quaternion.LookRotation(FSMMain.Object.dir - transform.position);
        //}

        //if (FSMMain.Object.WaitTime <= 0)
        //{
        //    FSMMain.Next();
        //}

    }

    public override void ExitState()
    {
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }


    public override void UpdateState()
    {
        FSMMain.SeeEnemy = FSMMain.ts.position;
        FSMMain.SeeEnemy.y = FSMMain.transform.position.y;


        if (FSMMain.Character.isGrounded)
        {
            curtime += Time.deltaTime;
            Vector3 vec = FSMMain.SeeEnemy, vec2 = FSMMain.transform.position;
            vec.y = 0;
            vec2.y = 0;
            Debug.Log($"{FSMMain.gameObject.name} Distance : {Vector3.Distance(vec, vec2)} > {FSMMain.ststed.stat._distance*10 - 0.05f}"); 
            if (Vector3.Distance(vec,vec2) > FSMMain.ststed.stat._distance*10 - 0.05f)
            {
                FSMMain.Object.InPos = false;
                FSMMain.ChangeState(FSMState.Move);
            }
            else if(curtime > 1f)
            {
                FSMMain.Object._skill = null;
                FSMMain.Object.InPos = true;
                FSMMain.ChangeState(FSMState.Skill);
            }
            //if (FSMMain.Object.WaitTime > 0 && TimeController.Instance.Timer == 1)
            //{
            //    if (curtime > FSMMain.Object.WaitTime)
            //    {
            //        curtime = 0;
            //        curringTime = true;
            //        FSMMain.Next();
            //    }
            //}
        } // 아레는 죽는거
        else
        {
            Vector3 velocity = Vector3.zero;
            velocity.y -= 9.8f * Time.deltaTime;
            FSMMain.Character.Move(10 * velocity * Time.deltaTime);
            DIeTime += Time.deltaTime;
            if (transform.position.y < -1f)
            {
                FSMMain.Character.Move(30 * velocity * Time.deltaTime);
                if (DIeTime > 1)
                {
                    FSMMain.ChangeState(FSMState.Death);
                }
            }
        }

       

        //UpdateAction?.Invoke();
    }
}

