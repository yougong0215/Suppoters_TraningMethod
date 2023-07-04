using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuckDownAction : CommonAction
{
    private bool isPushed = true;
    float curtime = 5;
    float DIeTime = 0;

    bool die = false;

    Vector3 knockbackDirection = Vector3.zero;
    float knockbackForce = 4f; // �˹� ��
    float knockbackDuration = 1f; // �˹� ���� �ð�
    float knockbackTimer = 0f;

    protected override void Init()
    {
        com.FSMMain.Object.useSkill = false;
        com.FSMMain.Character.enabled = true;
        com.FSMMain.AG.enabled = false;
        DIeTime = 0;
        curtime = 1;
        isPushed = true;
        knockbackDirection = com.FSMMain.transform.position - GameObject.FindGameObjectWithTag("Boss").transform.position;
        knockbackDirection.Normalize();
        Debug.Log($"knock : {knockbackDirection}");
        com.FSMMain.LookRotations(GameObject.FindGameObjectWithTag("Boss").transform.position);

    }
    protected override void OnEndFunc()
    {
        isPushed = false; // �˹� ���� �� ���� �ʱ�ȭ
        curtime = 0;
    }

    protected override void OnEventFunc()
    {
        isPushed = true;
        knockbackTimer = 0;
    }


    bool IsPositionOnNavMesh(Vector3 position)
    {
        UnityEngine.AI.NavMeshHit hit;
        return UnityEngine.AI.NavMesh.SamplePosition(position, out hit, 0.1f, UnityEngine.AI.NavMesh.AllAreas);
    }

    protected override void OnUpdateFunc()
    {
        curtime -= Time.deltaTime;

        if (knockbackTimer < knockbackDuration)
        {
            knockbackTimer += Time.deltaTime;
            com.FSMMain.Character.Move(knockbackDirection * knockbackForce * Time.deltaTime);
        }
        else
        {
            Vector3 velocity = Vector3.zero;
            velocity.y -= 9.8f * Time.deltaTime;
            com.FSMMain.Character.Move(velocity * Time.deltaTime);
        }

        
        if(die ==false)
        {
            if (IsPositionOnNavMesh(com.FSMMain.transform.position))
            {
                if (curtime < 0 && isPushed == false)
                {
                    com.FSMMain.useinged.Clear();
                    com.FSMMain.ChangeState(FSMState.WakeUp);
                }

            }
            else
            {
                die = true;
                StartCoroutine(Die());
            }
        }




    }

    IEnumerator Die()
    {   
        knockbackTimer  = 0;
        yield return new WaitForSeconds(1f);
        com.FSMMain.ChangeState(FSMState.Death);
    }




}
