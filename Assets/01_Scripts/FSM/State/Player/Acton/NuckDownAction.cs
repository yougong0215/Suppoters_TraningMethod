using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuckDownAction : CommonAction
{
    public float hitForce = 5f; // 맞았을 때의 힘의 세기
    public float pushDuration = 0.5f; // 밀려나는 시간
    public float pushSpeed = 5f; // 밀려나는 속도

    private bool isPushed = true;
    private Vector3 pushDirection;

    public float _wakeUPTime = 5;

    float curtime = 5;
    Coroutine co;
    protected override void OnEndFunc()
    {
        curtime = _wakeUPTime;
        isPushed = false;
    }

    protected override void OnEventFunc()
    {
        isPushed = true;
    }


    protected override void OnUpdateFunc()
    {
        if (isPushed == false)
            curtime -= Time.deltaTime;

        if(curtime < 0)
        {
            isPushed = true;
            com.FSMMain.ChangeState(FSMState.WakeUp);
        }

        

    }
}
