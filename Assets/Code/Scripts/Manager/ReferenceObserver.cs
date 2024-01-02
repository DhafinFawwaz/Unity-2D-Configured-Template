using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceObserver : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    [SerializeField] HUDManager _hudManager;

    void OnEnable()
    {
        _levelManager.OnSetRandomHighscore += _hudManager.UpdateScore;
    }
    void OnDisable()
    {
        _levelManager.OnSetRandomHighscore -= _hudManager.UpdateScore;
    }
}
