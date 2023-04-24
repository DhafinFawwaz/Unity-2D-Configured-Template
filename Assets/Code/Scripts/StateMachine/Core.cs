using UnityEngine;

public abstract class Core : MonoBehaviour
{
    public abstract string GetCurrentState();
    public abstract string GetPreviousState();
    public abstract void OnHurt();
}

public abstract class Core<TStates> : Core where TStates : States
{
    #region StateMachine
    TStates _states;
    BaseState<TStates> _currentState;
    BaseState<TStates> _previousState;


    public TStates States {get {return _states;} set {_states = value;}}
    public BaseState<TStates> CurrentState {get {return _currentState;} set {_currentState = value;}}
    public BaseState<TStates> PreviousState {get {return _previousState;} set {_previousState = value;}}
    public void SwitchState(BaseState<TStates> newState)
    {
        _previousState = CurrentState;
        _currentState.StateExit();
        _currentState = newState;
        _currentState.StateEnter();
    }
    public override string GetCurrentState()
    {
        return _currentState.ToString();
    }
    public override string GetPreviousState()
    {
        return _previousState.ToString();
    }

    public override void OnHurt()
    {
        _currentState.OnHurt();
    }
#endregion StateMachine
}
