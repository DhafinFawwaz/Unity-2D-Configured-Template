using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BatFlyState : BaseState<BatStates>
{
    public BatFlyState(BatCore contextCore, BatStates States) : base (contextCore, States)
    {
    }

    public override void StateEnter()
    {
        
    }

    public override void StateUpdate()
    {
        
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
