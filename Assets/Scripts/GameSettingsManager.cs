using UnityEngine;

public static class GameSettingsManager
{
    [Range(MIN_MOUSE_SENSITIVITY, MAX_MOUSE_SENSITIVITY)]
    private const float DEFAULT_MOUSE_SENSITIVITY = 0.15f;

    public const float MIN_MOUSE_SENSITIVITY = 0.01f;
    public const float MAX_MOUSE_SENSITIVITY = 0.3f;

    [Range(0.0001f, 1f)]
    private const float DEFAULT_VOLUME = 0.5f;

    private const int defaultGraphicsIndex = 0;
    //private const int defaultResolutionIndex = 0;

    private const string PLAYER_PREFS_MOUSE_SENSITIVITY = "mouseSensitivity";

    private const string PLAYER_PREFS_MAIN_VOLUME = "MainVolume";
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";

    private const string PLAYER_PREFS_GRAPHICS_INDEX = "GraphicsIndex";
    //private const string PLAYER_PREFS_RESOLUTION_INDEX = "ResolutionIndex";

    public static float MouseSensitivity { get; private set; }

    public static float MainVolume { get; private set; }
    public static float MusicVolume { get; private set; }
    public static float SoundVolume { get; private set; }

    public static int GraphicsIndex { get; private set; }
    //public static int ResolutionIndex { get; private set; }

    public static void LoadSettings()
    {
        MouseSensitivity = PlayerPrefs.GetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, DEFAULT_MOUSE_SENSITIVITY);

        MainVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MAIN_VOLUME, DEFAULT_VOLUME);
        MusicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, DEFAULT_VOLUME);
        SoundVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, DEFAULT_VOLUME);

        GraphicsIndex = PlayerPrefs.GetInt(PLAYER_PREFS_GRAPHICS_INDEX, defaultGraphicsIndex);
        //ResolutionIndex = PlayerPrefs.GetInt(PLAYER_PREFS_RESOLUTION_INDEX, defaultResolutionIndex);
    }

    /*
    public static void SaveSettings()
    {
        // All the PlayerPrefs.SetX method calls should later be put here
    }
    */

    public static void SetMouseSensitivity(float value)
    {
        MouseSensitivity = value;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, MouseSensitivity);
    }

    public static void SetVolume(VolumeController.VolumeType volumeType, float value)
    {
        switch (volumeType)
        {
            case VolumeController.VolumeType.MainVolume:
                MainVolume = value;
                PlayerPrefs.SetFloat(PLAYER_PREFS_MAIN_VOLUME, MainVolume);
                break;
            case VolumeController.VolumeType.MusicVolume:
                MusicVolume = value;
                PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, MusicVolume);
                break;
            case VolumeController.VolumeType.SoundVolume:
                SoundVolume = value;
                PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, SoundVolume);
                break;
        }
    }

    public static void SetGraphics(int index)
    {
        GraphicsIndex = index;
        PlayerPrefs.SetInt(PLAYER_PREFS_GRAPHICS_INDEX, GraphicsIndex);
        QualitySettings.SetQualityLevel(GraphicsIndex);
    }

    /*public static void SetResolution(int index)
    {
        ResolutionIndex = index;
        PlayerPrefs.SetInt(PLAYER_PREFS_RESOLUTION_INDEX, ResolutionIndex);
    }*/
}