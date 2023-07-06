using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct skillinfo
{
    public FSMState state;
    public SkillSO _skill;
    public Vector3 dir;
    public bool InPos;
    public bool useSkill;
}

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

    public skillinfo Object;
    public AgentStatus ststed;

    [SerializeField] public AttackBush bus;
    public bool Patton = false;

    public Vector3 SeeEnemy;
    public Transform ts;
    CapsuleCollider capsule;
    bool b = true;

    [NonSerialized] public bool AutoMove = true;
    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider>();
        _nav = GetComponent<NavMeshAgent>();
        _chara = GetComponent<CharacterController>();
        ststed = GetComponent<AgentStatus>();
        if(gameObject.name != "Boss")
        ts = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    public void Next(skillinfo sk)
    {
        Object = sk;
        ChangeState(Object.state);
    }
    public void LookRotations(Vector3 dir)
    {
        dir.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(dir - transform.position);

        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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

        if(Character.enabled ==false)
        {
            if(capsule)
            {
                capsule.enabled = true;
            }
        }
        else
        {
            if (capsule)
            {
                capsule.enabled = false;
            }
        }
        currentState?.UpdateState();

    }

    public bool CanSelect()
    {
        if (NowState() != FSMState.nuckdown && NowState() != FSMState.WakeUp && NowState() != FSMState.Death)
        {
            return true;
        }
        return false;
    }
}