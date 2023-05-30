using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillCommonAction : CommonAction
{
    [SerializeField] public SkillSO _skillSO;
    public void SetAnim()
    {
        com.AnimationCon.ChangeAnimationClip(FSMState.Skill, _skillSO.clips);
    }

    public void Destroy()
    {
        _skillSO = null;
    }
}
