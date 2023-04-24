using System.Collections.Generic;
public class BatStates : States
{
    BatCore _core;
    Dictionary<State, BaseState<BatStates>> _states = new Dictionary<State, BaseState<BatStates>>();
    
    enum State
    {
        Fly, Idle, Jump, 
    }
    public BatStates(BatCore contextCore)
    {
        _core = contextCore;

        _states[State.Fly] = new BatFlyState(_core, this);
        _states[State.Idle] = new BatIdleState(_core, this);
        _states[State.Jump] = new BatJumpState(_core, this);

    }

    public BaseState<BatStates> Fly() => _states[State.Fly];
    public BaseState<BatStates> Idle() => _states[State.Idle];
    public BaseState<BatStates> Jump() => _states[State.Jump];

    
}
