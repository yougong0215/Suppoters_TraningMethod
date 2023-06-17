using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM : MonoBehaviour
{
    [SerializeField]
    private Dictionary<FSMState, CommonState> stateMap = new Dictionary<FSMState, CommonState>();
    [SerializeField] private CommonState currentState;
    [SerializeField] private NavMeshAgent _nav;
    [SerializeField] private CharacterController _chara;

    public NavMeshAgent AG => _nav  ;

    public bool bStop = false;
    public CharacterController Character => _chara;

    [SerializeField] List<skillinfo> useinged = new List<skillinfo>();
    [SerializeField]skillinfo useing;

    public skillinfo Object => useing;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _chara = GetComponent<CharacterController>();
    }

    public void SetUseing(List<skillinfo> sk)
    {
        useinged = sk;
        Next();
    }

    public void Next()
    {
        if(useinged.Count > 0)
        {
            useing = useinged[0];
            useinged.RemoveAt(0);
            ChangeState(useing.state);
        }
        else
        {
            GameController.Contorller.StopPlayer();
        }
    }
    public void LookRotations(Vector3 dir)
    {
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir - transform.position);
    }

    public void AddState(FSMState state, CommonState stateObject)
    {
        stateMap[state] = stateObject;
        Debug.Log(stateMap[state]);
    }

    public void SetInitialState(FSMState initialState)
    {
        if (stateMap.TryGetValue(initialState, out CommonState stateObject))
        {
            currentState = stateObject;
            currentState.EnterState();
        }
        else
        {
            Debug.LogError("Invalid initial state: " + initialState);
        }
    }

    public FSMState NowState()
    {
        return currentState._myState;
    }

    public CommonState CurrentState => currentState;

    public void ChangeState(FSMState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        if (stateMap.TryGetValue(newState, out CommonState stateObject))
        {
            currentState = stateObject;
            currentState.EnterState();
        }
        else
        {
            Debug.LogError("Invalid state: " + newState);
        }
    }

    private void Update()
    {
        if(_chara.enabled == true)
        {
            Vector3 velocity = Vector3.zero;
            velocity.y -= 9.8f * Time.deltaTime;
            Character.Move(velocity * Time.deltaTime);

        }

        currentState?.UpdateState();

    }
}