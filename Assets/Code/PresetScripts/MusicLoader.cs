using System.Collections;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    void OnEnable()
    {
        if(Singleton.Instance == null)
        {
            StartCoroutine(DelayOnEnable());
            return;
        }

        if(musicClip == Singleton.Instance.audio.GetCurrentMusicClip())
        {}
        else if(musicClip != null)
            Singleton.Instance.audio.PlayMusic(musicClip);
        else 
            Singleton.Instance.audio.StopMusic();
    }

    IEnumerator DelayOnEnable()
    {
        yield return new WaitForEndOfFrame();
        if(musicClip != null)
            Singleton.Instance.audio.PlayMusic(musicClip);
        else Singleton.Instance.audio.StopMusic();
    }
}
