using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuckDownAction : CommonAction
{
    public float hitForce = 5f; // �¾��� ���� ���� ����
    public float pushDuration = 0.5f; // �з����� �ð�
    public float pushSpeed = 5f; // �з����� �ӵ�

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
