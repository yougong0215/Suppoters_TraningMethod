using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : CommonState
{
    [SerializeField] public SkillSO _skillSO;
    [SerializeField] float Distance = 0.5f;
    Vector3 dir;
    float curtime = -0.5f;
    public override void EnterState()
    {
        curtime = -0.5f;
        FSMMain.AG.enabled = false;
        FSMMain.Character.enabled = true;
        if (FSMMain.Object._skill)
            _skillSO = FSMMain.Object._skill;

        FSMMain.LookRotations(FSMMain.Object.dir);
        if (_skillSO != null)
        {
            PoolAble obj = PoolManager.Instance.Pop(_skillSO.SkillObj.name);


            if (FSMMain.Object.InPos)
            {
                obj.transform.position = FSMMain.transform.position;
            }
            else
            {

                obj.transform.position = FSMMain.Object.dir;
            }


            if (obj.GetComponent<DamageCaster>())
            {

                obj.GetComponent<DamageCaster>().Init((int)((FSMMain.ststed.stat.ATK + FSMMain.ststed.AddDamage) * FSMMain.ststed.himsDamage), FSMMain.ststed.stat.Critical + FSMMain.ststed.Cirt, FSMMain.ststed.stat.CriticalDamage + FSMMain.ststed.CirtDAM);
            }
            obj.transform.rotation = Quaternion.LookRotation(FSMMain.Object.dir - transform.position);
        }

        FSMMain.transform.position = FSMMain.Object.dir;
        StopDash();
    }

    public override void ExitState()
    {
        FSMMain.AG.enabled = true;
        FSMMain.Character.enabled = false;
        if (_skillSO != null)
        {
            PoolAble obj = PoolManager.Instance.Pop(_skillSO.SkillObj.name);

            if (FSMMain.Object.InPos)
            {
                obj.transform.position = FSMMain.transform.position;
            }
            else
            {

                obj.transform.position = FSMMain.Object.dir;
            }


            if (obj.GetComponent<DamageCaster>())
            {

                obj.GetComponent<DamageCaster>().Init((int)((FSMMain.ststed.stat.ATK + FSMMain.ststed.AddDamage) * FSMMain.ststed.himsDamage), FSMMain.ststed.stat.Critical + FSMMain.ststed.Cirt, FSMMain.ststed.stat.CriticalDamage + FSMMain.ststed.CirtDAM);

            }
            obj.transform.rotation = Quaternion.LookRotation(FSMMain.Object.dir - transform.position);

            GetBuff[] get = obj.GetComponentsInChildren<GetBuff>();
            foreach (GetBuff bt in get)
            {
                bt.transform.position = FSMMain.Object.dir;

            }
        }

    }

    public override void UpdateState()
    {
    }

    private void StopDash()
    {
        FSMMain.ChangeState(FSMState.Idle);
    }


}
