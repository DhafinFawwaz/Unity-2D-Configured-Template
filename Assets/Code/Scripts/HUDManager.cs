using UnityEngine;
using TMPro;
public class HUDManager : MonoBehaviour
{
    GameManager _game;
    [SerializeField] TextMeshProUGUI _highscoreText;
    public TextMeshProUGUI HighscoreText{get{return _highscoreText;}}

    void Awake()
    {
        _game = Singleton.Instance.Game;
        _game.HUD = this;
        _highscoreText.text = "Highscore: " + Save.Data.Highscore.ToString();
    }
}
