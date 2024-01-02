using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public static class ResolutionManager
{

#if UNITY_STANDALONE_WIN
    static Resolution[] _resolutions;
#elif UNITY_WEBGL
    static Resolution[] _resolutions = new Resolution[1];  
#elif UNITY_ANDROID
    static Resolution[] _resolutions = new Resolution[6]; // For some reason, Screen.resolitions won't return the available resolutions for some android devices. So this has to be done.
#endif   
    public static void Initialize()
    {
#if UNITY_STANDALONE_WIN
        _resolutions = Screen.resolutions.Select(resolution => 
        new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
#elif UNITY_WEBGL
        for(int i = 0; i < 1; i++)
        {
            _resolutions[i].height = Display.main.systemHeight*(i+1)/1;
            _resolutions[i].width = Display.main.systemWidth*(i+1)/1;
        }
#elif UNITY_ANDROID
        for(int i = 0; i < 6; i++)
        {
            _resolutions[i].height = Display.main.systemHeight*(i+1)/6;
            _resolutions[i].width = Display.main.systemWidth*(i+1)/6;
        }
#endif   
        int defaultWidth = Screen.currentResolution.width;
        int defaultHeight = Screen.currentResolution.height;

        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;
        for(int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " × " + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == defaultWidth && _resolutions[i].height == defaultHeight)
            {currentResolutionIndex = i;}
        }

        SetResolution(PlayerPrefs.GetInt("Resolution", currentResolutionIndex));
        SetFullScreen(
            IntToBool(PlayerPrefs.GetInt("IsFullScreen", 1))
        );
    }
    static bool IntToBool(int n) => n == 0 ? false : true;
    static int BoolToInt(bool b) => b == false ? 0 : 1;
    public static void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("IsFullScreen", BoolToInt(isFullScreen));
    }
    public static bool GetIsFullscreen()
    {
        return IntToBool(PlayerPrefs.GetInt("IsFullScreen", 0));
    }
    public static void SetResolution(int resolutionIndex)
    {
        if(_resolutions == null) return;
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }

    public static Resolution[] GetResolutions() => _resolutions;

}
