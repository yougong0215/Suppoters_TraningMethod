using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillAction : CommonAction
{
    AttackBush bs;
    [SerializeField] Transform ts;

    bool AnimENd = false;
    protected override void Init()
    {
        if (com.FSMMain.bus.anim != null)
            com.AnimationCon.ChangeAnimationClip(FSMState.Skill, com.FSMMain.bus.anim);
        AnimENd = false;
    }

    protected override void OnEndFunc()
    {
        AnimENd = true;
    }

    protected override void OnEventFunc()
    {
        do
        {
            bs = PoolManager.Instance.Pop(com.FSMMain.bus.name) as AttackBush;
            Vector3 vec;
            if (bs.FrontPos == false)
            {
                vec = com.FSMMain.transform.position;
            }
            else
            {
                vec = ts.position;
            }
            vec += new Vector3(0, 0.05f, 0);

            bs.transform.localEulerAngles = new Vector3(0, com.FSMMain.transform.localEulerAngles.y, 0);
            bs.Init((int)(com.FSMMain.ststed.AddDamage + com.FSMMain.ststed.stat.ATK)
                , com.FSMMain.ststed.Cirt + com.FSMMain.ststed.stat.Critical
                , com.FSMMain.ststed.CirtDAM + com.FSMMain.ststed.stat.CriticalDamage
                , vec);



            if (bs.nextBush != null && bs.FastNext == true)
            {
                com.FSMMain.bus = bs.nextBush;
            }

        } while (bs.nextBush != null && bs.FastNext);

    }

    protected override void OnUpdateFunc()
    {
        if(bs != null && bs.atk == true && AnimENd == true)
        {
            if (bs.nextBush != null && bs.FastNext == false)
            {
                com.FSMMain.bus = bs.nextBush;
                com.FSMMain.ChangeState(FSMState.Skill);
                bs = null;
            }
            else
            {
                com.FSMMain.ChangeState(FSMState.Idle);
                bs = null;
            }
        }
    }
}
