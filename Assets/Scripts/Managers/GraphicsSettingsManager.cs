using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettingsManager : MonoBehaviour
{
    public static GraphicsSettingsManager Instance { get; private set; }

    private Resolution[] availableResolutions;

    public List<string> ResolutionDropdownOptions { get; private set; }

    private double currentRefreshRate;

    private int startResolutionIndex;

    [SerializeField]
    private CameraBobController cameraBobController;

    [SerializeField]
    private FPSCounter fpsCounter;

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
        // GameSettingsManager.Instance.SetResolutionIndex(StartResolutionIndex);

        SetGraphics(GameSettingsManager.Instance.GraphicsIndex);
        
        if (GameSettingsManager.Instance.ResolutionIndex >= 0 && GameSettingsManager.Instance.ResolutionIndex < availableResolutions.Length)
        {
            SetResolution(GameSettingsManager.Instance.ResolutionIndex);
        }
        else
        {
            SetResolution(startResolutionIndex);
        }

        SetFullscreen(GameSettingsManager.Instance.Fullscreen);
        SetVSync(GameSettingsManager.Instance.VSync);
        SetFPSMax(GameSettingsManager.Instance.FPSMax);
        SetFPSDisplayEnabled(GameSettingsManager.Instance.FPSDisplay);
        SetHeadBobEnabled(GameSettingsManager.Instance.HeadBobEnabled);
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

    public void SetHeadBobEnabled(bool enabled)
    {
        if (cameraBobController != null)
        {
            cameraBobController.SetHeadBobEnabled(enabled);
        }
        GameSettingsManager.Instance.SetHeadBobEnabled(enabled);
    }

    public void SetFPSDisplayEnabled(bool enabled)
    {
        if (fpsCounter != null)
        {
            fpsCounter.SetFPSCounterActive(enabled);
        }
        GameSettingsManager.Instance.SetFPSDisplay(enabled);
    }

    private void SetupResolutionSettings()
    {
        Resolution[] screenResolutions = Screen.resolutions;
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        List<string> dropdownOptions = new();
        List<Resolution> validResolutionsList = new();

        startResolutionIndex = 0;

        foreach (var resolution in screenResolutions)
        {
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