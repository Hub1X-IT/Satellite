using UnityEngine;

public static class GameSettingsManager
{
    private const float defaultMouseSensitivity = 25f;

    [Range(0.0001f, 1f)]
    private const float defaultVolume = 0.5f;


    private const string PLAYER_PREFS_MOUSE_SENSITIVITY = "mouseSensitivity";

    private const string PLAYER_PREFS_MAIN_VOLUME = "MainVolume";
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";

    public static float MouseSensitivity { get; private set; }

    public static float MainVolume { get; private set; }
    public static float MusicVolume { get; private set; }
    public static float SoundVolume { get; private set; }


    public static void LoadSettings()
    {
        MouseSensitivity = PlayerPrefs.GetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, defaultMouseSensitivity);

        MainVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MAIN_VOLUME, defaultVolume);
        MusicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, defaultVolume);
        SoundVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, defaultVolume);
    }

    /*
    public static void SaveSettings()
    {
        // All the PlayerPrefs.SetX method calls maybe should be put here
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
}