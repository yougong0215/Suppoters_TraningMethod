using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterIdleState : CommonState
{
    public float curtime = 0;
    [SerializeField] public SkillSO _skillSO;
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


        if (FSMMain.Object._skill)
            _skillSO = FSMMain.Object._skill;

        if (_skillSO != null && _skillSO.dashAgain == true)
        {
            PoolAble obj = PoolManager.Instance.Pop(_skillSO.SkillObj.name);

            if (FSMMain.Object.Fire == false)
            {
                obj.transform.position = FSMMain.Object.dir;
            }
            else
            {
                obj.transform.position = FSMMain.Object.pos;
            }
            if (obj.GetComponent<DamageCaster>())
            {

                obj.GetComponent<DamageCaster>().Init((int)((FSMMain.ststed.stat.ATK + FSMMain.ststed.AddDamage) * FSMMain.ststed.himsDamage), FSMMain.ststed.stat.Critical + FSMMain.ststed.Cirt, FSMMain.ststed.stat.CriticalDamage + FSMMain.ststed.CirtDAM);
            }
            obj.transform.rotation = Quaternion.LookRotation(FSMMain.Object.dir - transform.position);
        }

        if (FSMMain.Object.WaitTime <= 0)
        {
            FSMMain.Next(false);
        }

    }

    public override void ExitState()
    {
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }


    public override void UpdateState()
    {
        


        if(FSMMain.Character.isGrounded)
        {
            curtime += Time.deltaTime;
            if (FSMMain.Object.WaitTime > 0 && TimeController.Instance.Timer == 1)
            {
                if (curtime > FSMMain.Object.WaitTime)
                {
                    curtime = 0;
                    curringTime = true;
                    FSMMain.Next(false);
                }
            }
        }
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

