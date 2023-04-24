using UnityEngine;

public class BatCore : Core<BatStates>
{
    void Awake()
    {
        States = new BatStates(this);
        CurrentState = States.Idle();
        CurrentState.StateEnter();
    }

    public override void OnHurt(HitParams hitParams)
    {
        base.OnHurt(hitParams);
    }


    void Update()
    {
        CurrentState.StateUpdate();
        if(Input.GetKeyDown(KeyCode.H))
        {
            GetComponent<Core>().OnHurt(new HitParams(10, 100, Vector2.up, Element.Fire, 0.5f));
        }
    }
    void FixedUpdate()
    {
        CurrentState.StateFixedUpdate();
    }

    
}
