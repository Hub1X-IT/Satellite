using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettingsManager : MonoBehaviour
{
    public static GraphicsSettingsManager Instance { get; private set; }

    private Resolution[] availableResolutions;

    public List<string> ResolutionDropdownOptions { get; private set; }

    private double currentRefreshRate;

    private int startResolutionIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(GraphicsSettingsManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        SetupResolutionSettings();
    }

    private void Start()
    {
        GameSettingsManager.Instance.SetResolutionIndex(startResolutionIndex);

        SetGraphics(GameSettingsManager.Instance.GraphicsIndex);
        SetResolution(GameSettingsManager.Instance.ResolutionIndex);
    }

    public void SetGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
        GameSettingsManager.Instance.SetGraphicsIndex(index);
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(availableResolutions[index].width, availableResolutions[index].height, Screen.fullScreen);
        GameSettingsManager.Instance.SetResolutionIndex(index);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        GameSettingsManager.Instance.SetFullscreen(fullscreen);
    }

    public void SetVSync(bool vsync)
    {
        QualitySettings.vSyncCount = vsync ? 1 : 0;
        GameSettingsManager.Instance.SetVSync(vsync);
    }

    public void SetFPSMax(int value)
    {
        Application.targetFrameRate = value;
        GameSettingsManager.Instance.SetFPSMax(value);
    }

    private void SetupResolutionSettings()
    {
        Resolution[] screenResolutions = Screen.resolutions;
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        List<string> dropdownOptions = new();
        List<Resolution> validResolutionsList = new();

        startResolutionIndex = 0;

        for (int i = 0; i < screenResolutions.Length; i++)
        {
            Resolution resolution = screenResolutions[i];
            if (resolution.refreshRateRatio.value == currentRefreshRate)
            {
                validResolutionsList.Add(resolution);
                dropdownOptions.Add($"{resolution.width} x {resolution.height}");
            }
        }

        availableResolutions = validResolutionsList.ToArray();

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            Resolution resolution = availableResolutions[i];
            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                startResolutionIndex = i;
            }
        }

        ResolutionDropdownOptions = dropdownOptions;
    }
}