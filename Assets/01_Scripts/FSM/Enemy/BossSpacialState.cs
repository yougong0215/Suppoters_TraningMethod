using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpacialState : CommonState
{
    AttackBush bs;
    bool a = false;

    public override void EnterState()
    {

        bs = PoolManager.Instance.Pop(FSMMain.bus.name) as AttackBush;
        bs.transform.localEulerAngles = new Vector3(0, FSMMain.transform.localEulerAngles.y, 0);
        bs.Init((int)(FSMMain.ststed.AddDamage + FSMMain.ststed.stat.ATK)
            , FSMMain.ststed.Cirt + FSMMain.ststed.stat.Critical
            , FSMMain.ststed.CirtDAM + FSMMain.ststed.stat.CriticalDamage
            , new Vector3(0,0.1f,0));


        FSMMain.Character.enabled = false;
        FSMMain.AG.enabled = false;
        FSMMain.transform.position = new Vector3(0, 50, 0);
        _animator.OnAnimationEndTrigger += EndAnim;
        FSMMain.Character.enabled = true;
    }

    public override void ExitState()
    {
        _animator.SetAttackAnimation(false);
        _animator.OnAnimationEndTrigger -= EndAnim;
    }

    public override void UpdateState()
    {
        Vector3 velocity = Vector3.zero;
        velocity.y -= 9.8f * Time.deltaTime;
        FSMMain.Character.Move(velocity);
        if(FSMMain.Character.isGrounded && a ==false)
        {

            a = true;
            AnimationCon.ChangeAnimationClip(FSMState.Skill, FSMMain.bus.anim);
            _animator.SetAttackAnimation(true);

        }
    }

    void EndAnim()
    {
        BossPattonGroundBreak.Instacne.GenerateNavmesh();
        FSMMain.ChangeState(FSMState.Idle);
    }

}
