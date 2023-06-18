using System.Collections.Generic;
using UnityEngine;

public enum FSMState
{
    Idle    = 1,
    Move    = 2,
    Dash  = 4,
    Hit     = 8,
    Faint   = 16, // De Buff
    nuckdown = 32,
    WakeUp  = 64,
    Death   = 128,
    Skill   = 256
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