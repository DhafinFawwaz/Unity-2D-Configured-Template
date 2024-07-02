using System.Collections;
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
        return PlayerPrefs.GetFloat("musicVolume", (float)3/5);
    }
    public float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("soundVolume", (float)3/5);
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

    public void SetMusicMixerVolume(float newVal)
    {
        if(newVal < 0.01f) _musicSource.mute = true;
        else _musicSource.mute = false;
        _musicMixer.SetFloat("Volume", 
            Mathf.Lerp(_minVolume, _maxVolume, Ease.OutCubic(newVal))
        );
        PlayerPrefs.SetFloat("musicVolume", newVal);
    }
    public void SetSoundMixerVolume(float newVal)
    {
        if(newVal < 0.01f) _soundSource.mute = true;
        else _soundSource.mute = false;
        _soundMixer.SetFloat("Volume", 
            Mathf.Lerp(_minVolume, _maxVolume, Ease.OutCubic(newVal))
        );
        PlayerPrefs.SetFloat("soundVolume", newVal);
    }

    public void ToggleLoop(bool isLooping)
    {
        _musicSource.loop = isLooping;
    }

    public void MusicFadeOut(float duration)
        => StartCoroutine(MusicFadeOutIEnumerator(duration));
        
    public void MusicFadeIn(float duration)
        => StartCoroutine(MusicFadeInIEnumerator(duration));

    IEnumerator MusicFadeInIEnumerator(float duration)
    {
        float t = 0;
        while(t <= 1)
        {
            _musicSource.volume = t;
            t += Time.unscaledDeltaTime/duration;
            yield return null;
        }
        _musicSource.volume = 0;
    }

    IEnumerator MusicFadeOutIEnumerator(float duration)
    {
        float t = 0;
        while(t <= 1)
        {
            _musicSource.volume = 1 - t;
            t += Time.unscaledDeltaTime/duration;
            yield return null;
        }
        _musicSource.volume = 0;
    }
    public void MusicFadeOutAndChangeTo(AudioClip _musicClip, bool isLooping, float duration, float delayBeforeChangeDuration)
        => StartCoroutine(MusicFadeOutAndChangeToIEnumerator(_musicClip, isLooping, duration, delayBeforeChangeDuration));
    IEnumerator MusicFadeOutAndChangeToIEnumerator(AudioClip _musicClip, bool isLooping, float duration, float delayBeforeChangeDuration)
    {
        float t = 0;
        while(t <= 1)
        {
            _musicSource.volume = 1 - t;
            t += Time.unscaledDeltaTime/duration;
            yield return null;
        }
        _musicSource.volume = 0;

        yield return new WaitForSecondsRealtime(delayBeforeChangeDuration);
        _musicSource.volume = 1;
        PlayMusic(_musicClip);
        ToggleLoop(isLooping);
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
            return;
        }
        PlaySound(SFX[index].Clip, SFX[index].Volume);
    }
}


public static class Audio
{
    static AudioManager _audio => Singleton.Instance.Audio;
    public static bool IsInitialized => Singleton.Instance != null;
    public static float GetMusicVolume() => _audio.GetMusicVolume();
    public static float GetSoundVolume() => _audio.GetSoundVolume();
    public static AudioClip GetCurrentMusicClip() => _audio.GetCurrentMusicClip();
    public static void PlayMusic(AudioClip audioClip) => _audio.PlayMusic(audioClip);
    public static void StopMusic() => _audio.StopMusic();
    public static void StopPlayMusic() => _audio.StopPlayMusic();
    public static void PlaySound(AudioClip audioClip, float volume) => _audio.PlaySound(audioClip, volume);
    public static void PlaySound(AudioClip audioClip) => _audio.PlaySound(audioClip);
    public static void SetMusicSourceVolume(float t) => _audio.SetMusicSourceVolume(t);
    public static void SetMusicMixerVolume(float newVal) => _audio.SetMusicMixerVolume(newVal);
    public static void SetSoundMixerVolume(float newVal) => _audio.SetSoundMixerVolume( newVal);
    public static void ToggleLoop(bool isLooping) => _audio.ToggleLoop(isLooping);
    public static void MusicFadeOut(float duration) => _audio.MusicFadeOut(duration);
    public static void MusicFadeIn(float duration) => _audio.MusicFadeIn(duration);
    public static void MusicFadeOutAndChangeTo(AudioClip _musicClip, bool isLooping, float duration, float delayBeforeChangeDuration)
        => _audio.MusicFadeOutAndChangeTo(_musicClip, isLooping, duration, delayBeforeChangeDuration);
    public static void PlaySound(int index) => _audio.PlaySound(index);



    static float _lastTimeSoundPlayed = Time.time;
    /// <summary>
    /// Play a sound with a time limit. If the sound is longer than the time limit, no sound will be played.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="timeLimit"></param>
    public static void PlaySoundWithTimeLimit(AudioClip clip, float timeLimit)
    {
        if(Time.time - _lastTimeSoundPlayed < timeLimit) return;
        _lastTimeSoundPlayed = Time.time;
        PlaySound(clip);
    }
}