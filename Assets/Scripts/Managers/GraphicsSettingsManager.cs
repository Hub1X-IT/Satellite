using System.Collections.Generic;
using UnityEngine;

public static class GraphicsSettingsManager
{
    private static Resolution[] availableResolutions;

    public static List<string> ResolutionDropdownOptions { get; private set; }

    public static float currentRefreshRate;

    public static void OnAwake()
    {
        SetupResolutionSettings();
    }

    public static void OnStart()
    {
        SetGraphics(GameSettingsManager.GraphicsIndex);
        SetResolution(GameSettingsManager.ResolutionIndex);
    }

    public static void SetGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
        GameSettingsManager.SetGraphicsIndex(index);
    }

    public static void SetResolution(int index)
    {
        Screen.SetResolution(availableResolutions[index].width, availableResolutions[index].height, Screen.fullScreen);
        GameSettingsManager.SetResolutionIndex(index);
    }

    public static void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        GameSettingsManager.SetFullscreen(fullscreen);
    }

    public static void SetVSync(bool vsync)
    {
        QualitySettings.vSyncCount = vsync ? 1 : 0;
        GameSettingsManager.SetVSync(vsync);
    }

    public static void SetFPSMax(int value)
    {
        Application.targetFrameRate = value;
        GameSettingsManager.SetFPSMax(value);
    }

    private static void SetupResolutionSettings()
    {
        Resolution[] screenResolutions = Screen.resolutions;
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        List<string> dropdownOptions = new();
        List<Resolution> validResolutionsList = new();

        for (int i = 0; i < screenResolutions.Length; i++)
        {
            Resolution resolution = screenResolutions[i];
            if (resolution.refreshRateRatio.value == currentRefreshRate)
            {
                validResolutionsList.Add(resolution);
                dropdownOptions.Add($"{resolution.width} x {resolution.height}");
            }

            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                GameSettingsManager.SetResolutionIndex(i);
            }
        }

        availableResolutions = validResolutionsList.ToArray();
        ResolutionDropdownOptions = dropdownOptions;
    }
}