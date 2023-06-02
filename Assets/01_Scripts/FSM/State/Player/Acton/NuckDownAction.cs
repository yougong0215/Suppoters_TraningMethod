using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuckDownAction : CommonAction
{
    public float hitForce = 5f; // 맞았을 때의 힘의 세기
    public float pushDuration = 0.5f; // 밀려나는 시간
    public float pushSpeed = 5f; // 밀려나는 속도

    private bool isPushed = false;
    private Vector3 pushDirection;

    public float _wakeUPTime = 5;

    Coroutine co;
    protected override void OnEndFunc()
    {
        co = StartCoroutine(WakeUpTime());
    }

    protected override void OnEventFunc()
    {
    }

    IEnumerator WakeUpTime()
    {
        yield return new WaitForSeconds(_wakeUPTime);

        com.FSMMain.ChangeState(FSMState.WakeUp);
    }

    protected override void OnUpdateFunc()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            com.FSMMain.ChangeState(FSMState.nuckdown);
            StopCoroutine(co);
        }

    }
}
