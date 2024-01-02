using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputObserver : MonoBehaviour
{
    void OnEnable()
    {
        SceneTransition.s_onBeforeOut += DisableInput;
        SceneTransition.s_onAfterIn += EnableInput;
        AnimationUI.OnSetActiveAllInput += GlobalInput.SetActiveAllInput;
    }
    void OnDisable()
    {
        SceneTransition.s_onBeforeOut -= DisableInput;
        SceneTransition.s_onAfterIn -= EnableInput;
        AnimationUI.OnSetActiveAllInput -= GlobalInput.SetActiveAllInput;
    }
    void DisableInput()
        => GlobalInput.SetActiveAllInput(false);
    
    
    void EnableInput()
        => GlobalInput.SetActiveAllInput(true);
}
