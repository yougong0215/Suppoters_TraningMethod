using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillCommonAction : CommonAction
{
    [SerializeField] public SkillSO _skillSO;
    [SerializeField] public SkillSO _NormalAttack;

    bool Arainge = false;
    float Changecurtime = 2;
    float curTime = 0;
    
    public void SetAnim()
    {
        curTime = 0;
        Arainge = false;

        if(com.FSMMain.Object._skill != null)
        {
            _skillSO = com.FSMMain.Object._skill;
            com.FSMMain.LookRotations(com.FSMMain.Object.dir);
        }
        else
        {
            _skillSO = _NormalAttack;
            com.FSMMain.SeeEnemy = com.FSMMain.ts.position;
            com.FSMMain.SeeEnemy.y = transform.position.y;
            com.FSMMain.LookRotations(com.FSMMain.SeeEnemy);
        }

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
        if (com.FSMMain.Object.InPos)
        {
            obj.transform.position = com.FSMMain.transform.position;
            obj.transform.rotation = com.FSMMain.transform.rotation;
        }
        else
        {
            obj.transform.rotation = Quaternion.LookRotation(com.FSMMain.Object.dir - com.FSMMain.transform.position);
            obj.transform.position = com.FSMMain.Object.dir;
        }

        if (obj.GetComponent<DamageCaster>())
        {
            Debug.Log($"{com.FSMMain.gameObject.name} CRIIN d : {com.FSMMain.ststed.stat.CriticalDamage + com.FSMMain.ststed.CirtDAM}");
            obj.GetComponent<DamageCaster>().Init((int)((com.FSMMain.ststed.stat.ATK + com.FSMMain.ststed.AddDamage) * com.FSMMain.ststed.himsDamage) , com.FSMMain.ststed.stat.Critical + com.FSMMain.ststed.Cirt, com.FSMMain.ststed.stat.CriticalDamage + com.FSMMain.ststed.CirtDAM);
        }


        GetBuff[] get = obj.GetComponentsInChildren<GetBuff>();
        foreach(GetBuff bt in get)
        {
            if (com.FSMMain.Object.InPos)
                bt.transform.position = com.FSMMain.transform.position;
            else
            {
                bt.transform.position = com.FSMMain.Object.dir;
            }
            
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
