using UnityEngine;

public class HUDManager : MonoBehaviour
{
    GameManager _game;

    void Awake()
    {
        _game = Singleton.Instance.Game;
        _game.HUD = this;
        
    }
}
