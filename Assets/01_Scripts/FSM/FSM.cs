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
    public bool Nexte = false;
    public NavMeshAgent AG => _nav  ;

    public bool bStop = false;
    public CharacterController Character => _chara;

    [SerializeField] public List<skillinfo> useinged = new List<skillinfo>();
    [SerializeField] skillinfo useing;
    public AgentStatus ststed;

    [SerializeField] public AttackBush bus;
    public bool Patton = false;
    public skillinfo Object => useing;

    public Vector3 SeeEnemy;
    public Transform ts;

    bool b = true;
    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _chara = GetComponent<CharacterController>();
        ststed = GetComponent<AgentStatus>();
    }

    public void SetUseing(List<skillinfo> sk)
    {
        useinged = sk;
        Next(true);
    }

    Coroutine co = null;

    public void Next(bool a)
    {
        if(a==true)
        {
            b = true;
        }

        if(b == true)
        {
            b = false;
            StartCoroutine(delay());
            //Debug.Log($"{gameObject.name} : {useinged.Count}");
        }

    }

    public IEnumerator delay()
    {

        yield return null;
        if (TimeController.Instance.Timer == 1)
        {
            if (NowState() == FSMState.Death)
            {
                GameController.Contorller.StopPlayer(ststed.pl);
                b = false;

            }
            else
            {
                yield return new WaitUntil(() => NowState() == FSMState.Idle);
                if (useinged.Count > 0)
                {

                    useing = useinged[0];
                    ChangeState(useing.state);
                    useinged.RemoveAt(0);
                    b = true;
                }
                else
                {


                    GameController.Contorller.StopPlayer(ststed.pl);
                    b = true;

                }
            }
           

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
        if (Input.GetKeyDown(KeyCode.P))
        {

            if (ststed.pl == players.None)
                ststed.HP = (int)(ststed.stat.HP * 0.2f);
        }
        currentState?.UpdateState();

    }
}