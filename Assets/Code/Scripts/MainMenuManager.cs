using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //Freely edit this whole thing
    [SerializeField] GameObject home;
    [SerializeField] GameObject settings;
    [SerializeField] float delay = 0;
    public void DisableAll()
    {
        home.SetActive(false);
        settings.SetActive(false);
    }
    public void LoadHome()
    {
        Singleton.Instance.transition.Out()
            .AddOutEnd(Singleton.Instance.transition.InDefault)
            .SetDelayAfterOut(delay)
            .AddOutEnd(ActivateHome);
    }
    void ActivateHome()
    {
        DisableAll();
        home.SetActive(true);
    }
    public void LoadSettings()
    {
        Singleton.Instance.transition.Out()
            .AddOutEnd(Singleton.Instance.transition.InDefault)
            .SetDelayAfterOut(delay)
            .AddOutEnd(ActivateSettings);
        
    }
    void ActivateSettings()
    {
        DisableAll();
        settings.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        Singleton.Instance.scene.LoadSceneWithTransition(sceneName);
    }
}
