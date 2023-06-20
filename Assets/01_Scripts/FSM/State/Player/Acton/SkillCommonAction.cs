using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillCommonAction : CommonAction
{
    [SerializeField] public SkillSO _skillSO;

    bool Arainge = false;
    float Changecurtime = 2;
    float curTime = 0;
    
    public void SetAnim()
    {
        curTime = 0;
        Arainge = false;
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
        Arainge = true;
        curTime = 0;
        com.FSMMain.ChangeState(FSMState.Idle);
    }


    protected override void OnEventFunc()
    {
        PoolAble obj = PoolManager.Instance.Pop(_skillSO.SkillObj.name);
        Debug.Log($"DMG : ({com.FSMMain.ststed.stat.ATK } + {com.FSMMain.ststed.AddDamage}) * { com.FSMMain.ststed.himsDamage} = {(int)((com.FSMMain.ststed.stat.ATK + com.FSMMain.ststed.AddDamage) * com.FSMMain.ststed.himsDamage)}");
        if (com.FSMMain.Object.Fire == false)
        {
            obj.transform.position = com.FSMMain.Object.dir;
        }
        else
        {
            obj.transform.position = com.FSMMain.Object.pos;
        }
        if (obj.GetComponent<DamageCaster>())
        {
            Debug.Log($"{com.FSMMain.gameObject.name} CRIIN d : {com.FSMMain.ststed.stat.CriticalDamage + com.FSMMain.ststed.CirtDAM}");
            obj.GetComponent<DamageCaster>().Init((int)((com.FSMMain.ststed.stat.ATK + com.FSMMain.ststed.AddDamage) * com.FSMMain.ststed.himsDamage) , com.FSMMain.ststed.stat.Critical + com.FSMMain.ststed.Cirt, com.FSMMain.ststed.stat.CriticalDamage + com.FSMMain.ststed.CirtDAM);
        }
        obj.transform.rotation = Quaternion.LookRotation(com.FSMMain.Object.dir - transform.position);

        GetBuff[] get = obj.GetComponentsInChildren<GetBuff>();
        foreach(GetBuff bt in get)
        {
            bt.transform.position = com.FSMMain.Object.dir;
            
        }
       
    }

    protected override void OnUpdateFunc()
    {
        curTime += Time.deltaTime;
        if(Arainge)
        {
            Debug.Log(com.FSMMain.name);
        }
        if(curTime > Changecurtime && Arainge == true)
        {
            //Arainge = false;
            //curTime = 0;
            com.FSMMain.ChangeState(FSMState.Idle);
        }
    }

    protected override void Init()
    {
        SetAnim();
    }
}
