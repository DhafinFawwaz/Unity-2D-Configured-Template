using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    //Freely edit this whole thing
    [SerializeField] GameObject _home;
    [SerializeField] GameObject _settings;
    [SerializeField] TextMeshProUGUI _highscoreText;
    [SerializeField] float _delay = 0;
    [SerializeField] SceneTransition _sceneTransition;
    void Start()
    {
        _highscoreText.text = "Highscore: "+Save.Data.Highscore.ToString();

    }
    public void LoadScene(string sceneName)
    {
        // _sceneTransition.StartSceneTransition(sceneName); // this one for without loading bar
        SceneLoader.LoadSceneWithProgressBar(sceneName);
    }

    public void DisableAll()
    {
        _home.SetActive(false);
        _settings.SetActive(false);
    }

    public void LoadHome()
    {
        _sceneTransition.StartTransitionWithoutLoadingScene()
            .SetDelayAfterOut(_delay)
            .AddListenerAfterOut(ActivateHome);
    }
    void ActivateHome()
    {
        DisableAll();
        _home.SetActive(true);
    }

    public void LoadSettings()
    {
        _sceneTransition.StartTransitionWithoutLoadingScene()
            .SetDelayAfterOut(_delay)
            .AddListenerAfterOut(ActivateSettings);
    }
    void ActivateSettings()
    {
        DisableAll();
        _settings.SetActive(true);
    }


    public void ExitGame() => Application.Quit();


    
}
