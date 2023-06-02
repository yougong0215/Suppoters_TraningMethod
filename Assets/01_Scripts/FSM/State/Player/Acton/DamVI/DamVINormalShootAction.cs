using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamVINormalShootAction : SkillCommonAction
{
    protected override void OnEndFunc()
    {
        com.FSMMain.ChangeState(FSMState.Idle);
    }


    protected override void OnEventFunc()
    {
        GameObject obj = Instantiate(_skillSO.SkillObj, transform);
    }

    protected override void OnUpdateFunc()
    {

    }
}
