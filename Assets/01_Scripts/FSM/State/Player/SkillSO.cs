using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skill")]
public class SkillSO : ScriptableObject
{
    public AnimationClip clips;

    public PoolAble SkillObj;

    public bool dashAgain = false;
}

