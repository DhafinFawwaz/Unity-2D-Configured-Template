using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    GameManager _game;
    [ReadOnly] public PlayerLocomotion Locomotion;
    [ReadOnly] public PlayerStats Stats;

    void Awake()
    {
        _game = Singleton.Instance.Game;
        _game.Core = this;
        Locomotion = GetComponent<PlayerLocomotion>();
        Stats = GetComponent<PlayerStats>();
    }
}
