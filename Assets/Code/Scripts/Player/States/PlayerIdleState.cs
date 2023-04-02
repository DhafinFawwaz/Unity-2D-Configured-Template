using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerIdleState : BaseState<PlayerCore, PlayerStates>
{
    public PlayerIdleState(PlayerCore contextCore, PlayerStates States) : base (contextCore, States)
    {
    }

    public override void StateEnter()
    {
        
    }

    public override void StateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Core.SwitchState(States.Jump());
    }
    public override void StateFixedUpdate()
    {

    }

    public override void StateExit()
    {
        
    }
}
