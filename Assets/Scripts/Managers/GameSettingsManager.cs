using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance { get; private set; }

    [Range(MinMouseSensitivity, MaxMouseSensitivity)]
    private const float DefaultMouseSensitivity = 0.15f;

    public const float MinMouseSensitivity = 0.01f;
    public const float MaxMouseSensitivity = 0.3f;

    [Range(0.0001f, 1f)]
    private const float DefaultVolume = 1f;

    private const int DefaultGraphicsIndex = 0;
    private const int DefaultResolutionIndex = 0;
    private const int DefaultFullscreenIndex = 1;
    private const int DefaultVSyncIndex = 1;
    private const int DefaultFPSMax = 0;
    private const int DefaultFPSDisplayIndex = 0;
    private const int DefaultHeadBobEnabled = 1;

    private const string PlayerPrefs_MouseSensitivity = "MouseSensitivity";

    private const string PlayerPrefs_MainVolume = SoundManager.MasterVolume;
    private const string PlayerPrefs_MusicVolume = SoundManager.MusicVolume;
    private const string PlayerPrefs_SoundFXVolume = SoundManager.SoundFXVolume;
    private const string PlayerPrefs_DialogueVolume = SoundManager.DialogueVolume;

    private const string PlayerPrefs_GraphicsIndex = "GraphicsIndex";
    private const string PlayerPrefs_ResolutionIndex = "ResolutionIndex";
    private const string PlayerPrefs_FullscreenIndex = "FullscreenIndex";
    private const string PlayerPrefs_VSyncIndex = "VSyncIndex";
    private const string PlayerPrefs_FPSMax = "FPSMax";
    private const string PlayerPrefs_FPSDisplayIndex = "FPSDisplayIndex";
    private const string PlayerPrefs_HeadBobEnabled = "HeadBobEnabled";

    public float MouseSensitivity { get; private set; }

    public float MainVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float SoundVolume { get; private set; }
    public float DialogueVolume { get; private set; }

    public int GraphicsIndex { get; private set; }
    public int ResolutionIndex { get; private set; }

    public int FPSMax { get; private set; }
    public bool FPSDisplay { get; private set; }

    public bool Fullscreen { get; private set; }
    public bool VSync { get; private set; }
    public bool HeadBobEnabled { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(GameSettingsManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadSettings();
    }

    private void LoadSettings()
    {
        MouseSensitivity = PlayerPrefs.GetFloat(PlayerPrefs_MouseSensitivity, DefaultMouseSensitivity);

        MainVolume = PlayerPrefs.GetFloat(PlayerPrefs_MainVolume, DefaultVolume);
        MusicVolume = PlayerPrefs.GetFloat(PlayerPrefs_MusicVolume, DefaultVolume);
        SoundVolume = PlayerPrefs.GetFloat(PlayerPrefs_SoundFXVolume, DefaultVolume);
        DialogueVolume = PlayerPrefs.GetFloat(PlayerPrefs_DialogueVolume, DefaultVolume);

        GraphicsIndex = PlayerPrefs.GetInt(PlayerPrefs_GraphicsIndex, DefaultGraphicsIndex);
        ResolutionIndex = PlayerPrefs.GetInt(PlayerPrefs_ResolutionIndex, DefaultResolutionIndex);
        Fullscreen = PlayerPrefs.GetInt(PlayerPrefs_FullscreenIndex, DefaultFullscreenIndex) == 1;
        VSync = PlayerPrefs.GetInt(PlayerPrefs_VSyncIndex, DefaultVSyncIndex) == 1;
        FPSMax = PlayerPrefs.GetInt(PlayerPrefs_FPSMax, DefaultFPSMax);
        FPSDisplay = PlayerPrefs.GetInt(PlayerPrefs_FPSDisplayIndex, DefaultFPSDisplayIndex) == 1;
        HeadBobEnabled = PlayerPrefs.GetInt(PlayerPrefs_HeadBobEnabled, DefaultHeadBobEnabled) == 1;
    }


    public void SetMouseSensitivity(float value)
    {
        MouseSensitivity = value;
        PlayerPrefs.SetFloat(PlayerPrefs_MouseSensitivity, MouseSensitivity);
    }

    public void SetVolume(SoundManager.Volume volumeType, float value)
    {
        switch (volumeType)
        {
            case SoundManager.Volume.MasterVolume:
                MainVolume = value;
                PlayerPrefs.SetFloat(PlayerPrefs_MainVolume, MainVolume);
                break;
            case SoundManager.Volume.MusicVolume:
                MusicVolume = value;
                PlayerPrefs.SetFloat(PlayerPrefs_MusicVolume, MusicVolume);
                break;
            case SoundManager.Volume.SoundVolume:
                SoundVolume = value;
                PlayerPrefs.SetFloat(PlayerPrefs_SoundFXVolume, SoundVolume);
                break;
            case SoundManager.Volume.DialogueVolume:
                DialogueVolume = value;
                PlayerPrefs.SetFloat(PlayerPrefs_DialogueVolume, DialogueVolume);
                break;
        }
    }

    public void SetGraphicsIndex(int index)
    {
        GraphicsIndex = index;
        PlayerPrefs.SetInt(PlayerPrefs_GraphicsIndex, GraphicsIndex);
    }

    public void SetResolutionIndex(int index)
    {
        ResolutionIndex = index;
        PlayerPrefs.SetInt(PlayerPrefs_ResolutionIndex, ResolutionIndex);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Fullscreen = fullscreen;
        PlayerPrefs.SetInt(PlayerPrefs_FullscreenIndex, Fullscreen ? 1 : 0);
    }

    public void SetVSync(bool vsync)
    {
        VSync = vsync;
        PlayerPrefs.SetInt(PlayerPrefs_VSyncIndex, VSync ? 1 : 0);
    }

    public void SetFPSMax(int value)
    {
        FPSMax = value;
        PlayerPrefs.SetInt(PlayerPrefs_FPSMax, FPSMax);
    }

    public void SetFPSDisplay(bool display)
    {
        FPSDisplay = display;
        PlayerPrefs.SetInt(PlayerPrefs_FPSDisplayIndex, FPSDisplay ? 1 : 0);
    }

    public void SetHeadBobEnabled(bool enabled)
    {
        HeadBobEnabled = enabled;
        PlayerPrefs.SetInt(PlayerPrefs_HeadBobEnabled, HeadBobEnabled ? 1 : 0);
    }
}