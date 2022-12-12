using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [ReadOnly] public LevelManager Level;
    [ReadOnly] public HUDManager HUD;
    [ReadOnly] public PlayerCore Core;

    public void SetActiveAllInput(bool isActive)
    {
        transform.GetChild(0).gameObject.SetActive(!isActive);
    }

    
}
