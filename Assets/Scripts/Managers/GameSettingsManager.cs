using UnityEngine;

public static class GameSettingsManager
{
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

    private const string PlayerPrefs_MouseSensitivity = "MouseSensitivity";

    private const string PlayerPrefs_MainVolume = VolumeController.MasterVolume;
    private const string PlayerPrefs_MusicVolume = VolumeController.MusicVolume;
    private const string PlayerPrefs_SoundFXVolume = VolumeController.SoundFXVolume;

    private const string PlayerPrefs_GraphicsIndex = "GraphicsIndex";
    private const string PlayerPrefs_ResolutionIndex = "ResolutionIndex";
    private const string PlayerPrefs_FullscreenIndex = "FullscreenIndex";
    private const string PlayerPrefs_VSyncIndex = "VSyncIndex";
    private const string PlayerPrefs_FPSMax = "FPSMax";
    private const string PlayerPrefs_FPSDisplayIndex = "FPSDisplayIndex";

    public static float MouseSensitivity { get; private set; }

    public static float MainVolume { get; private set; }
    public static float MusicVolume { get; private set; }
    public static float SoundVolume { get; private set; }

    public static int GraphicsIndex { get; private set; }
    public static int ResolutionIndex { get; private set; }

    public static int FPSMax { get; private set; }
    public static bool FPSDisplay { get; private set; }

    public static bool Fullscreen { get; private set; }
    public static bool VSync { get; private set; }

    public static void LoadSettings()
    {
        MouseSensitivity = PlayerPrefs.GetFloat(PlayerPrefs_MouseSensitivity, DefaultMouseSensitivity);

        MainVolume = PlayerPrefs.GetFloat(PlayerPrefs_MainVolume, DefaultVolume);
        MusicVolume = PlayerPrefs.GetFloat(PlayerPrefs_MusicVolume, DefaultVolume);
        SoundVolume = PlayerPrefs.GetFloat(PlayerPrefs_SoundFXVolume, DefaultVolume);

        GraphicsIndex = PlayerPrefs.GetInt(PlayerPrefs_GraphicsIndex, DefaultGraphicsIndex);
        ResolutionIndex = PlayerPrefs.GetInt(PlayerPrefs_ResolutionIndex, DefaultResolutionIndex);
        Fullscreen = PlayerPrefs.GetInt(PlayerPrefs_FullscreenIndex, DefaultFullscreenIndex) == 1;
        VSync = PlayerPrefs.GetInt(PlayerPrefs_VSyncIndex, DefaultVSyncIndex) == 1;
        FPSMax = PlayerPrefs.GetInt(PlayerPrefs_FPSMax, DefaultFPSMax);
        FPSDisplay = PlayerPrefs.GetInt(PlayerPrefs_FPSDisplayIndex, DefaultFPSDisplayIndex) == 1;
    }


    public static void SetMouseSensitivity(float value)
    {
        MouseSensitivity = value;
        PlayerPrefs.SetFloat(PlayerPrefs_MouseSensitivity, MouseSensitivity);
    }

    public static void SetVolume(VolumeController.Volume volumeType, float value)
    {
        switch (volumeType)
        {
            case VolumeController.Volume.MasterVolume:
                MainVolume = value;
                PlayerPrefs.SetFloat(PlayerPrefs_MainVolume, MainVolume);
                break;
            case VolumeController.Volume.MusicVolume:
                MusicVolume = value;
                PlayerPrefs.SetFloat(PlayerPrefs_MusicVolume, MusicVolume);
                break;
            case VolumeController.Volume.SoundVolume:
                SoundVolume = value;
                PlayerPrefs.SetFloat(PlayerPrefs_SoundFXVolume, SoundVolume);
                break;
        }
    }

    public static void SetGraphicsIndex(int index)
    {
        GraphicsIndex = index;
        PlayerPrefs.SetInt(PlayerPrefs_GraphicsIndex, GraphicsIndex);
    }

    public static void SetResolutionIndex(int index)
    {
        ResolutionIndex = index;
        PlayerPrefs.SetInt(PlayerPrefs_ResolutionIndex, ResolutionIndex);
    }

    public static void SetFullscreen(bool fullscreen)
    {
        Fullscreen = fullscreen;
        PlayerPrefs.SetInt(PlayerPrefs_FullscreenIndex, Fullscreen ? 1 : 0);
    }

    public static void SetVSync(bool vsync)
    {
        VSync = vsync;
        PlayerPrefs.SetInt(PlayerPrefs_VSyncIndex, VSync ? 1 : 0);
    }

    public static void SetFPSMax(int value)
    {
        FPSMax = value;
        PlayerPrefs.SetInt(PlayerPrefs_FPSMax, FPSMax);
    }
    
    public static void SetFPSDisplay(bool display)
    {
        FPSDisplay = display;
        PlayerPrefs.SetInt(PlayerPrefs_FPSDisplayIndex, FPSDisplay ? 1 : 0);
    }
}