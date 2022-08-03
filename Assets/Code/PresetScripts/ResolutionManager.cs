using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class ResolutionManager : MonoBehaviour
{
    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => 
        new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " × " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {currentResolutionIndex = i;}
        }

        SetResolution(PlayerPrefs.GetInt("Resolution", currentResolutionIndex));
        SetFullScreen(
            IntToBool(PlayerPrefs.GetInt("IsFullScreen", 1))
        );
    }
    bool IntToBool(int n){return n == 0 ? false : true;}
    int BoolToInt(bool b){return b == false ? 0 : 1;}
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("IsFullScreen", BoolToInt(isFullScreen));
    }
    public bool GetIsFullscreen()
    {
        return IntToBool(PlayerPrefs.GetInt("IsFullScreen", 0));
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }
    public void SetResolutionPercentage(int numerator, int denominator)
    {
        int width = Display.main.systemWidth * numerator / denominator;
        int height = Display.main.systemHeight * numerator / denominator;
        Screen.SetResolution(width, height, true);
    }

    public Resolution[] GetResolutions()
    {
        return resolutions;
    }

}
