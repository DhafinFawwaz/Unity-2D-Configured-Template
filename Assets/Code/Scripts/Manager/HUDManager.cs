using UnityEngine;
using TMPro;
public class HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _highscoreText;
    public TextMeshProUGUI HighscoreText{get{return _highscoreText;}}

    void Awake()
    {
        _highscoreText.text = "Highscore: " + Save.Data.Highscore.ToString();
    }
    public void UpdateScore(int newScore)
    {
        _highscoreText.text = "Highscore: " + newScore.ToString();
    }
}
