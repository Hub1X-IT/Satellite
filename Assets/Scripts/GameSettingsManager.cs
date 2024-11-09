using UnityEngine;

public static class GameSettingsManager
{
    [Range(MinMouseSensitivity, MaxMouseSensitivity)]
    private const float DefaultMouseSensitivity = 0.15f;

    public const float MinMouseSensitivity = 0.01f;
    public const float MaxMouseSensitivity = 0.3f;

    [Range(0.0001f, 1f)]
    private const float DefaultVolume = 0.5f;

    private const int DefaultGraphicsIndex = 0;
    private const int DefaultResolutionIndex = 0;

    private const string PlayerPrefs_MouseSensitivity = "MouseSensitivity";

    private const string PlayerPrefs_MainVolume = VolumeController.MasterVolume;
    private const string PlayerPrefs_MusicVolume = VolumeController.MusicVolume;
    private const string PlayerPrefs_SoundFXVolume = VolumeController.SoundFXVolume;

    private const string PlayerPrefs_GraphicsIndex = "GraphicsIndex";
    private const string PlayerPrefs_ResolutionIndex = "ResolutionIndex";

    public static float MouseSensitivity { get; private set; }

    public static float MainVolume { get; private set; }
    public static float MusicVolume { get; private set; }
    public static float SoundVolume { get; private set; }

    public static int GraphicsIndex { get; private set; }
    public static int ResolutionIndex { get; private set; }

    public static void LoadSettings()
    {
        MouseSensitivity = PlayerPrefs.GetFloat(PlayerPrefs_MouseSensitivity, DefaultMouseSensitivity);

        MainVolume = PlayerPrefs.GetFloat(PlayerPrefs_MainVolume, DefaultVolume);
        MusicVolume = PlayerPrefs.GetFloat(PlayerPrefs_MusicVolume, DefaultVolume);
        SoundVolume = PlayerPrefs.GetFloat(PlayerPrefs_SoundFXVolume, DefaultVolume);

        GraphicsIndex = PlayerPrefs.GetInt(PlayerPrefs_GraphicsIndex, DefaultGraphicsIndex);
        ResolutionIndex = PlayerPrefs.GetInt(PlayerPrefs_ResolutionIndex, DefaultResolutionIndex);
    }

    
    public static void SaveSettings()
    {
        // All the PlayerPrefs.SetX method calls should later be put here
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
}