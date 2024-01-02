using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Linq;
public class SettingsManager : MonoBehaviour
{
    [SerializeField] SliderUI _musicSlider;
    [SerializeField] SliderUI _soundSlider;

    Resolution[] _resolutions;
    [SerializeField] TMP_Dropdown _resolutionDropdown;
    [SerializeField] Toggle _fullscreenToggle;

    void OnEnable()
    {
        _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundValueChanged);
    }
    void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundValueChanged);
    }
    void Start()
    {
        // Audio slider
        float musicVolume = Mathf.Lerp(_musicSlider.minValue, _musicSlider.maxValue, Audio.GetMusicVolume());
        float soundVolume = Mathf.Lerp(_soundSlider.minValue, _soundSlider.maxValue, Audio.GetSoundVolume());
        _musicSlider.SetValueWithoutNotify(musicVolume);
        _soundSlider.SetValueWithoutNotify(soundVolume);

        // Resolution dropdown
        _resolutions = ResolutionManager.GetResolutions();
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;
        for(int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " × " + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {currentResolutionIndex = i;}
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        // Fullscreen toggle
        _fullscreenToggle.isOn = Screen.fullScreen;
        ResolutionManager.SetFullScreen(_fullscreenToggle.isOn);
        _fullscreenToggle.onValueChanged.AddListener(ResolutionManager.SetFullScreen);
    }

    void OnMusicValueChanged(float newVal)
    {
        float normalizedVal = Mathf.InverseLerp(_musicSlider.minValue, _musicSlider.maxValue, newVal);
        Audio.SetMusicMixerVolume(normalizedVal);
    }

    void OnSoundValueChanged(float newVal)
    {
        float normalizedVal = Mathf.InverseLerp(_soundSlider.minValue, _soundSlider.maxValue, newVal);
        Audio.SetSoundMixerVolume(normalizedVal);
    }

    public void OnResolutionValueChanged(int resolutionIndex)
        => ResolutionManager.SetResolution(resolutionIndex);
}
