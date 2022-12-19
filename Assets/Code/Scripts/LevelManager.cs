using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    GameManager _game;
    public void LoadScene(string sceneName)
    {
        Singleton.Instance.Scene.LoadSceneWithTransition(sceneName);
        Singleton.Instance.Transition.SetMusicFade(sceneName == "MainMenu" ? true : false);
    }

    void Awake()
    {
        _game = Singleton.Instance.Game;
    }
    void Start()
    {
        Save.LoadData();
    }

    public void SetRandomHighscore()
    {
        int randomNumber = Random.Range(10, 200);
        Save.Data.Highscore = randomNumber;
        Save.SaveData(); 
        Singleton.Instance.Game.HUD.HighscoreText.text = "Highscore: " + randomNumber.ToString();
    }
    
}
