using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : CommonState
{
    bool death = false;
    public override void EnterState()
    {
       
        FSMMain.gameObject.tag = "Player2";
        if(death ==false)
        {
            death = true;

             _animator.SetNuckDownAnimation(true);
        }
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        Vector3 velocity = Vector3.zero;
        velocity.y -= 9.8f * Time.deltaTime;
        FSMMain.ststed.HP = 0;
        FSMMain.Character.Move(30 * velocity * Time.deltaTime);
    }
}
