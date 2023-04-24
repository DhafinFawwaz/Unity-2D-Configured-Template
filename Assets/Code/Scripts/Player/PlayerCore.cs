using UnityEngine;

public class PlayerCore : Core<PlayerStates>
{
    GameManager _game;
    [ReadOnly] public PlayerLocomotion Locomotion;
    [ReadOnly] public PlayerStats Stats;

    void Awake()
    {
        States = new PlayerStates(this);
        CurrentState = States.Idle();
        CurrentState.StateEnter();


        _game = Singleton.Instance.Game;
        _game.Core = this;
        Locomotion = GetComponent<PlayerLocomotion>();
        Stats = GetComponent<PlayerStats>();
    }

    public override void OnHurt()
    {
        
    }


    void Update()
    {
        CurrentState.StateUpdate();
        if(Input.GetKeyDown(KeyCode.H))
        {
            GetComponent<Core>().OnHurt();
        }
    }
    void FixedUpdate()
    {
        CurrentState.StateFixedUpdate();
    }

    
}
