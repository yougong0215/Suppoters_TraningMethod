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
    /// 자유롭게 움직이는상태
    /// </summary>
    Free,
    /// <summary>
    /// 이동상태
    /// </summary>
    Rally,
    /// <summary>
    /// 자리에서 공격상태
    /// </summary>
    AllOutAttack,
    
}