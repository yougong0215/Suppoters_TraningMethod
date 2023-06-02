using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuckDownAction : CommonAction
{
    public float hitForce = 5f; // �¾��� ���� ���� ����
    public float pushDuration = 0.5f; // �з����� �ð�
    public float pushSpeed = 5f; // �з����� �ӵ�

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
