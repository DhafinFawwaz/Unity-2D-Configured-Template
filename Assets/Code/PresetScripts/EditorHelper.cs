using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorHelper : MonoBehaviour
{



    public void TransitionOut()
    {
        Debug.Log("Transition Out");
        Singleton.Instance.transition.Out()
            // .SetOnAnimation(Singleton.Instance.transition.anim.OutAnimation)
        ;
    }
    
    public void TransitionIn()
    {
        Debug.Log("Transition In");
        Singleton.Instance.transition.In()
        ;
    }

    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject settingsCanvas;
    void OnEnableMainMenu()
    {
        DisableAllCanvas();
        mainMenuCanvas.SetActive(true);
    }

    void OnEnableSettings()
    {
        DisableAllCanvas();
        settingsCanvas.SetActive(true);
    }
    void DisableAllCanvas()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings");
        Singleton.Instance.transition.Out()
            .AddOutEnd(Singleton.Instance.transition.InDefault)
            .AddOutEnd(OnEnableSettings)
        ;
    }
    public void LoadMainMenu()
    {
        Debug.Log("Loading MainMenu");
        Singleton.Instance.transition.Out()
            .AddOutEnd(Singleton.Instance.transition.InDefault)
            .AddOutEnd(OnEnableMainMenu)
        ;
    }
    
}
