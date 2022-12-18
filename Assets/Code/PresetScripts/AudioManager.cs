using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] float _minVolume = -50f;
    [SerializeField] float _maxVolume = 10f;

    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _soundSource;
    [SerializeField] AudioMixer _musicMixer;
    [SerializeField] AudioMixer _soundMixer;
    [SerializeField] AudioClip _defaultSound;
    void Start()
    {
        _musicMixer.SetFloat("Volume", 
            Mathf.Lerp(_minVolume, _maxVolume, Ease.OutCubic(
                GetMusicVolume()
            ))
        );
        _soundMixer.SetFloat("Volume", 
            Mathf.Lerp(_minVolume, _maxVolume, Ease.OutCubic(
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
        return _musicSource.clip;
    }

    public void PlayMusic(AudioClip audioClip)
    {
        _musicSource.clip = audioClip;
        _musicSource.Stop();
        _musicSource.Play();
    }
    public void StopMusic()
    {
        _musicSource.clip = null;
        _musicSource.Stop();
    }
    public void StopPlayMusic()
    {
        _musicSource.Stop();
        _musicSource.Play();
    }


    public void PlaySound(AudioClip audioClip, float volume)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        _soundSource.PlayOneShot(audioClip, volume);
    }
    public void PlaySound(AudioClip audioClip)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        _soundSource.PlayOneShot(audioClip);
    }
    void PlayDefaultSound()
    {
        if(_defaultSound != null)
        _soundSource.PlayOneShot(_defaultSound);
    }

    
    public void SetMusicSourceVolume(float t)
    {
        _musicSource.volume = t;
    }

    public void OnMusicValueChanged(float newVal)
    {
        _musicMixer.SetFloat("Volume", 
            Mathf.Lerp(_minVolume, _maxVolume, Ease.OutCubic(newVal))
        );
        PlayerPrefs.SetFloat("musicVolume", newVal);
    }
    public void OnSoundValueChanged(float newVal)
    {
        _soundMixer.SetFloat("Volume", 
            Mathf.Lerp(_minVolume, _maxVolume, Ease.OutCubic(newVal))
        );
        PlayerPrefs.SetFloat("soundVolume", newVal);
    }


    // SFX
    [System.Serializable] public class Sound
    {
        [Tooltip("Clip to play")]public AudioClip Clip;
        [Tooltip("Volume of the clip")]
        public float Volume = 1;
        #if UNITY_EDITOR 
        [Tooltip("Just for naming, this isn't actually used anywhere")]public string ClipName;
        #endif
    }
    public Sound[] SFX;
    public void PlaySound(int index)
    {
        if(index > SFX.Length-1)
        {
            Debug.LogWarning("Please assign the clip at index " + index.ToString());
        }
        PlaySound(SFX[index].Clip, SFX[index].Volume);
    }
}
