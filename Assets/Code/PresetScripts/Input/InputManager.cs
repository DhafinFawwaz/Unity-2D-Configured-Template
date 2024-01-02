using UnityEngine;
public class GlobalInput
{
    static InputManager _input => Singleton.Instance.Input;
    public static void SetActiveAllInput(bool isActive) => _input.SetActiveAllInput(isActive);
}
public class InputManager : MonoBehaviour
{
    public void SetActiveAllInput(bool isActive)
    {
        transform.GetChild(0).gameObject.SetActive(!isActive);
    }

    
}
