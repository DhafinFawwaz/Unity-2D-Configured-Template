using System.Collections.Generic;
public class PlayerStates : IStates
{
    PlayerCore _core;
    Dictionary<State, BaseState<PlayerStates>> _states = new Dictionary<State, BaseState<PlayerStates>>();
    
    enum State
    {
        Idle,
        Jump,
    }
    public PlayerStates(PlayerCore contextCore)
    {
        _core = contextCore;

        _states[State.Idle] = new PlayerIdleState(_core, this);
        _states[State.Jump] = new PlayerJumpState(_core, this);
    }

    public BaseState<PlayerStates> Idle() => _states[State.Idle];
    public BaseState<PlayerStates> Jump() => _states[State.Jump];
    
}
