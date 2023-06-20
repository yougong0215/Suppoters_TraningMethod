using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : CommonState
{

    public override void EnterState()
    {
        _animator.SetAttackAnimation(true);
        _animator.OnAnimationEventTrigger += EventAction;
        _animator.OnAnimationEndTrigger += EndAction;
        Init?.Invoke();
        //if (transform.GetChild(0).gameObject.GetComponent<SkillCommonAction>())
        //{
        //    transform.GetChild(0).gameObject.GetComponent<SkillCommonAction>().SetAnim();
        //}
    }

    public override void ExitState()
    {
        _animator.SetAttackAnimation(false);
        _animator.OnAnimationEventTrigger -= EventAction;
        _animator.OnAnimationEndTrigger -= EndAction;
    }

    public override void UpdateState()
    {
        UpdateAction?.Invoke();
    }
}
