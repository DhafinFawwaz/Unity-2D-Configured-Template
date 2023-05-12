public abstract class BaseState<TStates> where TStates : IStates
{
    protected Core<TStates> _core;
    protected TStates _states;
    protected Core<TStates> Core{get{return _core;}}
    protected TStates States{get{return _states;}}
    public BaseState(Core<TStates> contextCore, TStates playerStates)
    {
        _core = contextCore;
        _states = playerStates;
    }
    public abstract void StateEnter();
    public abstract void StateUpdate();
    public abstract void StateFixedUpdate();
    public abstract void StateExit();
    public virtual void OnHurt(HitParams hitParams)
    {
        
    }
    protected void SwitchState(BaseState<TStates> newState)
    {
        _core.SwitchState(newState);
    }
}