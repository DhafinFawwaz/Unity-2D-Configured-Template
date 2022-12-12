using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    GameManager _game;
    [SerializeField] TextMeshProUGUI _highscoreText;
    public void LoadScene(string sceneName)
    {
        Singleton.Instance.Scene.LoadSceneWithTransition(sceneName);
        Singleton.Instance.Transition.SetMusicFade(sceneName == "MainMenu" ? true : false);
    }

    void Awake()
    {
        _game = Singleton.Instance.Game;
        Debug.Log(_game);
    }
    void Start()
    {
        Save.LoadData();
        _highscoreText.text = "Highscore: " + Save.Data.Highscore.ToString();
    }

    public void SetRandomHighscore()
    {
        int randomNumber = Random.Range(10, 200);
        _highscoreText.text = "Highscore: " + randomNumber.ToString();
        Save.Data.Highscore = randomNumber;
        Save.SaveData(); 
    }
    
}
