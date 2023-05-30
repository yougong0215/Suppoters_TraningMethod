using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInputMovement : MonoBehaviour
{
    NavMeshAgent _nav;
    FSM fsm;

    private void Awake()
    {
        _nav = transform.parent.GetComponent<NavMeshAgent>();
        fsm = transform.parent.GetComponent<FSM>();
    }

    public void StopPathFinding()
    {
        _nav.SetDestination(transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && (fsm.NowState() == FSMState.Idle || fsm.NowState() == FSMState.Move))
        {
            Ray ray = GameManager.Instance.Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 mousePosition = hit.point;
                _nav.SetDestination(mousePosition);

                if (fsm.NowState() == FSMState.Idle)
                    fsm.ChangeState(FSMState.Move);

            }



        }
    }

    // 플레이어의 이동 방향을 바라보도록 합니다.


}
