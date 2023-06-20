using System;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, IState
{
    public FSMState _myState;
    protected FSM fsm;
    public FSM FSMMain => fsm;
    protected AnimationController _animator;
    public AnimationController AnimationCon => _animator;
    protected Transform _parent;

    public Action Init = null;
    public Action UpdateAction = null;
    public Action EventAction = null;
    public Action EndAction = null;

    protected void OnEnable() 
    {
        _parent = transform.parent.parent;
        
        fsm = _parent.GetComponent<FSM>();

        _animator = transform.parent.parent.Find("Visual").GetComponent<AnimationController>();

        fsm.AddState(_myState, this);
    }


    public abstract void EnterState();
    public abstract void UpdateState();

    public abstract void ExitState();

}