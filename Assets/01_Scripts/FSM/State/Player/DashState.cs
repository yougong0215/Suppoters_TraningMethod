using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : CommonState
{
    [SerializeField] public SkillSO _skillSO;
    [SerializeField] float Distance = 0.5f;
    Vector3 dir;
    float curtime = -0.5f;

    bool animEnd = false;

    public override void EnterState()
    {
        AnimationCon.SetDashAnimation(true);

        AnimationCon.OnAnimationEventTrigger += EventAction;
        AnimationCon.OnAnimationEndTrigger += EndAction;
        curtime = -0.5f;
        FSMMain.AG.enabled = false;
        FSMMain.Character.enabled = true;
        if (FSMMain.Object._skill)
            _skillSO = FSMMain.Object._skill;

        FSMMain.LookRotations(FSMMain.Object.dir);
        if(_skillSO != null && _skillSO.dashAgain == true)
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
       
        
    }

    private void Awake()
    {
        EventAction += AnimEvent;
    }

    public void AnimEvent()
    {
        animEnd = true;
    }

    public override void ExitState()
    {
        AnimationCon.SetDashAnimation(false);
        FSMMain.AG.enabled = true;
        FSMMain.Character.enabled = false;

        AnimationCon.OnAnimationEventTrigger -= EventAction;
        AnimationCon.OnAnimationEndTrigger -= EndAction;

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
        if(animEnd)
        {
            Vector3 vec1 = FSMMain.transform.position;
            Vector3 vec2 = FSMMain.Object.dir;
            vec2.y = 0;
            vec1.y = 0;
            curtime += Time.deltaTime;

            dir = (FSMMain.Object.dir - FSMMain.transform.position);

            // 대쉬 지속 시간 체크
            Debug.Log($"dfd : {vec1}, {vec2} = {Vector3.Distance(vec1, vec2)}");
            if (Vector3.Distance(vec1, vec2) < 0.02f || Vector3.Distance(vec1, vec2) < curtime)
            {
                StopDash();

            }
            else
            {


                // 대쉬 방향으로 이동
                if (curtime < 0)
                    FSMMain.Character.Move(dir * 6 * Time.deltaTime);
                //FSMMain.LookRotations(FSMMain.Object.dir);
            }
        }
        


    }

    private void StopDash()
    {
        FSMMain.ChangeState(FSMState.Idle);
    }

}
