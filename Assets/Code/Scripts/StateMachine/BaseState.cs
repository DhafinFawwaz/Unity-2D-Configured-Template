public abstract class BaseState<TCore, TStates> where TCore : Core where TStates : States
{
    protected TCore _core;
    protected TStates _states;
    protected TCore Core{get{return _core;}}
    protected TStates States{get{return _states;}}
    public BaseState(TCore contextCore, TStates playerStates)
    {
        _core = contextCore;
        _states = playerStates;
    }
    public abstract void StateEnter();
    public abstract void StateUpdate();
    public abstract void StateFixedUpdate();
    public abstract void StateExit();
}