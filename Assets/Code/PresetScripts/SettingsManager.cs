using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Linq;
public class SettingsManager : MonoBehaviour
{
    [SerializeField] SliderUI musicSlider;
    [SerializeField] SliderUI soundSlider;

    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;
    void Start()
    {
        musicSlider.SetValueInstant(Singleton.Instance.audio.GetMusicVolume());
        soundSlider.SetValueInstant(Singleton.Instance.audio.GetSoundVolume());
        
        musicSlider.OnVariableChanged += OnMusicValueChanged;
        soundSlider.OnVariableChanged += OnSoundValueChanged;

        resolutions = Singleton.Instance.resolution.GetResolutions();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " × " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {currentResolutionIndex = i;}
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Singleton.Instance.resolution.GetIsFullscreen();
    }

    void OnMusicValueChanged(float newVal)
    {
        Singleton.Instance.audio.OnMusicValueChanged(newVal);
    }

    void OnSoundValueChanged(float newVal)
    {
        Singleton.Instance.audio.OnSoundValueChanged(newVal);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Singleton.Instance.resolution.SetFullScreen(isFullScreen);
    }
    public void OnResolutionValueChanged(int resolutionIndex)
    {
        Singleton.Instance.resolution.SetResolution(resolutionIndex);
    }
}
