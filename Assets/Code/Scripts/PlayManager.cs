using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreText;
    public void LoadScene(string sceneName)
    {
        Singleton.Instance.loader.LoadSceneWithTransition(sceneName);
        Singleton.Instance.transition.SetMusicFade(false);
    }

    void Start()
    {
        Singleton.Instance.save.LoadData();
        highscoreText.text = "Highscore: " + Singleton.Instance.save.data.highscore.ToString();
    }

    public void SetRandomHighscore()
    {
        int randomNumber = Random.Range(10, 200);
        highscoreText.text = "Highscore: " + randomNumber.ToString();
        Singleton.Instance.save.data.highscore = randomNumber;
        Singleton.Instance.save.SaveData(); 
    }
    
}
