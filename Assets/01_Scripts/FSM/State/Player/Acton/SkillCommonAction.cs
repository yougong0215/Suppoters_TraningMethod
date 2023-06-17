using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommonAction : CommonAction
{
    [SerializeField] public SkillSO _skillSO;
    public void SetAnim()
    {
        _skillSO = com.FSMMain.Object._skill;
        com.FSMMain.LookRotations(com.FSMMain.Object.dir);
        com.AnimationCon.ChangeAnimationClip(FSMState.Skill, _skillSO.clips);
    }

    public void Destroy()
    {
        _skillSO = null;
    }
    protected override void OnEndFunc()
    {
        com.FSMMain.ChangeState(FSMState.Idle);
        com.FSMMain.Next();
    }


    protected override void OnEventFunc()
    {
        DamageCaster obj = PoolManager.Instance.Pop(_skillSO.SkillObj.name) as DamageCaster;
        obj.Init((int)(com.FSMMain.ststed.stat.ATK + com.FSMMain.ststed.AddDamage), com.FSMMain.ststed.stat.Critical + com.FSMMain.ststed.Cirt, com.FSMMain.ststed.stat.CriticalDamage + com.FSMMain.ststed.CirtDAM);
        if(com.FSMMain.Object.Fire==false)
        {
            obj.transform.position = com.FSMMain.Object.dir;
        }
        else
        {
            obj.transform.position = com.FSMMain.Object.pos;
        }
        obj.transform.rotation = Quaternion.LookRotation(com.FSMMain.Object.dir - transform.position);
    }

    protected override void OnUpdateFunc()
    {

    }
}
