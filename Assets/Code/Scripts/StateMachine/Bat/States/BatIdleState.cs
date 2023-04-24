using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BatIdleState : BaseState<BatStates>
{
    public BatIdleState(BatCore contextCore, BatStates States) : base (contextCore, States)
    {
    }

    public override void StateEnter()
    {
        
    }

    public override void StateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SwitchState(States.Jump());
    }
    public override void StateFixedUpdate()
    {

    }

    public override void StateExit()
    {
        
    }
    public override void OnHurt(HitParams hitParams)
    {
        base.OnHurt(hitParams);
    }
}
