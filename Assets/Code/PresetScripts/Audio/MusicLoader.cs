using System.Collections;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    [SerializeField] AudioClip _musicClip;
    [SerializeField] bool _isLooping = true;
    void OnEnable() => StartCoroutine(DelayOnEnable());

    IEnumerator DelayOnEnable()
    {
        while(!Audio.IsInitialized)
        {
            Debug.Log("Audio isn't initialized yet");
            yield return null;
        }
        Audio.SetMusicSourceVolume(1);
        if(_musicClip == Audio.GetCurrentMusicClip()) // Same music
        {
            Audio.ToggleLoop(true);
        }
        else if(_musicClip != null) // Different music
        {
            Audio.ToggleLoop(_isLooping);
            Audio.PlayMusic(_musicClip);
        }
        else // No music
        {
            Audio.ToggleLoop(true);
            Audio.StopMusic();
        }
    }
    
    public void FadeOutMusic()
    {
        Audio.MusicFadeOut(0.5f);
    }
}
