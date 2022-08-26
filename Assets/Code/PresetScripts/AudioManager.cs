using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] float minVolume = -50f;
    [SerializeField] float maxVolume = 10f;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer soundMixer;
    [SerializeField] AudioClip defaultSound;
    void Start()
    {
        musicMixer.SetFloat("Volume", 
            Mathf.Lerp(minVolume, maxVolume, Ease.OutCubic(
                GetMusicVolume()
            ))
        );
        soundMixer.SetFloat("Volume", 
            Mathf.Lerp(minVolume, maxVolume, Ease.OutCubic(
                GetSoundVolume()
            ))
        );
    }
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("musicVolume", (float)5/7);
    }
    public float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("soundVolume", (float)5/7);
    }

    public AudioClip GetCurrentMusicClip()
    {
        return musicSource.clip;
    }

    public void PlayMusic(AudioClip audioClip)
    {
        musicSource.clip = audioClip;
        musicSource.Stop();
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.clip = null;
        musicSource.Stop();
    }
    public void StopPlayMusic()
    {
        musicSource.Stop();
        musicSource.Play();
    }


    public void PlaySound(AudioClip audioClip, float volume)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        soundSource.PlayOneShot(audioClip, volume);
    }
    public void PlaySound(AudioClip audioClip)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        soundSource.PlayOneShot(audioClip);
    }
    void PlayDefaultSound()
    {
        soundSource.PlayOneShot(defaultSound);
    }

    
    public void SetMusicSourceVolume(float t)
    {
        musicSource.volume = t;
    }

    public void OnMusicValueChanged(float newVal)
    {
        musicMixer.SetFloat("Volume", 
            Mathf.Lerp(minVolume, maxVolume, Ease.OutCubic(newVal))
        );
        PlayerPrefs.SetFloat("musicVolume", newVal);
    }
    public void OnSoundValueChanged(float newVal)
    {
        soundMixer.SetFloat("Volume", 
            Mathf.Lerp(minVolume, maxVolume, Ease.OutCubic(newVal))
        );
        PlayerPrefs.SetFloat("soundVolume", newVal);
    }
}
