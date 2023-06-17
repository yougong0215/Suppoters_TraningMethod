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
        obj.Init(Random.Range(6000,12000),1,1);
        obj.transform.position = com.FSMMain.Object.dir;
        obj.transform.rotation = Quaternion.LookRotation(com.FSMMain.Object.dir - transform.position);
    }

    protected override void OnUpdateFunc()
    {

    }
}
