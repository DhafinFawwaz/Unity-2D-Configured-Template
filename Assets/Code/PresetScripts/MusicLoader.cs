using System.Collections;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    [SerializeField] AudioClip _musicClip;
    void OnEnable() => StartCoroutine(DelayOnEnable());

    IEnumerator DelayOnEnable()
    {
        while(Singleton.Instance == null)
        {
            Debug.Log("Singleton isn't initialized yet");
            yield return null;
        }
        if(_musicClip == Singleton.Instance.Audio.GetCurrentMusicClip()) // Same music
        {}
        else if(_musicClip != null) // Different music
            Singleton.Instance.Audio.PlayMusic(_musicClip);
        else // No music
            Singleton.Instance.Audio.StopMusic();
    }
}
