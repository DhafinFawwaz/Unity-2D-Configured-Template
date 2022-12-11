using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    EventSystem _currentEventSystem;
    public void SetActiveAllInput(bool isActive)
    {
        if(_currentEventSystem == null)_currentEventSystem = EventSystem.current;
        _currentEventSystem.enabled = isActive;
    }
}
