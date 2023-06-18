using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : CommonState
{
    bool isDashing;
    public override void EnterState()
    {
        FSMMain.AG.enabled = false;
        FSMMain.Character.enabled = true;

    }

    public override void ExitState()
    {
        FSMMain.AG.enabled = true;
        FSMMain.Character.enabled = false;
    }

    public override void UpdateState()
    {
        Vector3 vec1 = FSMMain.transform.position;
        Vector3 vec2 = FSMMain.Object.dir;
        vec2.y = 0;
        vec1.y = 0;

        // 대쉬 지속 시간 체크
        Debug.Log($"dfd : {vec1}, {vec2} = {Vector3.Distance(vec1, vec2)}");
        if (Vector3.Distance(vec1, vec2) < 0.5f)
        {
            StopDash();

        }


        // 대쉬 방향으로 이동
        FSMMain.Character.Move((FSMMain.Object.dir- transform.position) * 6 * Time.deltaTime);

    }

    private void StopDash()
    {
        FSMMain.Next();
    }

}
