using System.Collections.Generic;
public class PlayerStates : States
{
    enum State
    {
        Idle,
        Jump,
    }
    PlayerCore _core;
    Dictionary<State, BaseState<PlayerCore, PlayerStates>> _states = 
        new Dictionary<State, BaseState<PlayerCore, PlayerStates>>();
    public PlayerStates(PlayerCore contextCore)
    {
        _core = contextCore;

        _states[State.Idle] = new PlayerIdleState(_core, this);
        _states[State.Jump] = new PlayerJumpState(_core, this);
    }

    public BaseState<PlayerCore, PlayerStates> Idle() => _states[State.Idle];
    public BaseState<PlayerCore, PlayerStates> Jump() => _states[State.Jump];
    
}
