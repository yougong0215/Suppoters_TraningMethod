using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private readonly int _isRunHash = Animator.StringToHash("is_move");
    private readonly int _RunTriggerhash = Animator.StringToHash("run");

    private readonly int _isAttackHash = Animator.StringToHash("is_Attack");
    private readonly int _AttackTriggerhash = Animator.StringToHash("attack");

    private readonly int _isHitHash = Animator.StringToHash("is_hit");
    private readonly int _hitTriggerhash = Animator.StringToHash("hit");

    public event Action OnAnimationEndTrigger = null;
    public event Action OnAnimationEventTrigger = null;
    public event Action OnPreAnimationEventTrigger = null;


    [SerializeField] AnimatorOverrideController AOC;
    [SerializeField] Avatar av;

    private Animator _animator;
    public Animator Animator => _animator;
    public AnimatorOverrideController _changeAnimation => AOC;

    FSM _fsm;
    //이미지 메이킹
    private void Awake()
    {
            _animator = GetComponent<Animator>();

        _animator.avatar = av;
        _animator.runtimeAnimatorController = AOC;

        
        _fsm = transform.parent.GetComponent<FSM>();
    }

    public void ChangeAnimationClip(FSMState fsm, AnimationClip clip)
    {
        AOC[fsm.ToString()] = clip;
    }

    public void OnAnimationEvent()
    {
        OnAnimationEventTrigger?.Invoke();
    }
    public void OnAnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }

    public void SetMoveAnimation(bool b)
    {
        _animator.SetBool(_isRunHash, b);
    }

    public void SetAttackAnimation(bool b)
    {
        if(b== true)
        {
            _animator.SetBool(_isAttackHash, b);
            _animator.SetTrigger(_AttackTriggerhash);
        }
        else
        {
            _animator.SetBool(_isAttackHash, b);
            _animator.ResetTrigger(_AttackTriggerhash);
        }
    }
}
