using System.Collections.Generic;
using UnityEngine;

public enum FSMState
{
    Idle    = 1,
    Move    = 2,
    Attack  = 4,
    Hit     = 8,
    Faint   = 16, // De Buff
    NuckDown= 32,
    Death   = 64,
    Skill   = 128
}

public enum BuddyState
{
    /// <summary>
    /// �����Ӱ� �����̴»���
    /// </summary>
    Free,
    /// <summary>
    /// �̵�����
    /// </summary>
    Rally,
    /// <summary>
    /// �ڸ����� ���ݻ���
    /// </summary>
    AllOutAttack,
    
}