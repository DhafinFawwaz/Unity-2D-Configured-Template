using UnityEngine;

public class PlayerCore : Core
{
#region StateMachine
    PlayerStates _states;
    BaseState<PlayerCore, PlayerStates> _currentState;
    BaseState<PlayerCore, PlayerStates> _previousState;


    public PlayerStates States {get {return _states;} set {_states = value;}}
    public BaseState<PlayerCore, PlayerStates> CurrentState {get {return _currentState;} set {_currentState = value;}}
    public BaseState<PlayerCore, PlayerStates> PreviousState {get {return _previousState;} set {_previousState = value;}}
    public void SwitchState(BaseState<PlayerCore, PlayerStates> newState)
    {
        _previousState = CurrentState;
        _currentState.StateExit();
        _currentState = newState;
        _currentState.StateEnter();
    }
    public override string GetState()
    {
        return _currentState.ToString();
    }
#endregion StateMachine

    
    GameManager _game;
    [ReadOnly] public PlayerLocomotion Locomotion;
    [ReadOnly] public PlayerStats Stats;

    void Awake()
    {
        _states = new PlayerStates(this);
        _currentState = _states.Idle();
        _currentState.StateEnter();



        _game = Singleton.Instance.Game;
        _game.Core = this;
        Locomotion = GetComponent<PlayerLocomotion>();
        Stats = GetComponent<PlayerStats>();
    }


    void Update()
    {
        CurrentState.StateUpdate();
    }
    void FixedUpdate()
    {
        CurrentState.StateFixedUpdate();
    }

    
}
