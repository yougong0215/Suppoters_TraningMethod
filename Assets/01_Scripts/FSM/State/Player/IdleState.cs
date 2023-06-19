using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : CommonState
{
    float curtime = 0;
    [SerializeField] public SkillSO _skillSO;
    bool curringTime = false;
    private void Start()
    {
        fsm.ChangeState(_myState);
    }

    public override void EnterState()
    {
        curtime = 0;
        _animator.OnAnimationEndTrigger += EndAction;
        _animator.OnAnimationEventTrigger += EventAction;

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
        curtime += Time.deltaTime;
        if(FSMMain.Object.WaitTime > 0 && TimeController.Instance.Timer == 1)
        {
            if (curtime > FSMMain.Object.WaitTime)
            {
                curtime = 0;
                curringTime = true;
                FSMMain.Next(false);
            }
        }


        //UpdateAction?.Invoke();
    }
}
