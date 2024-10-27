using UnityEngine;

public static class GameSettingsManager
{
    private static float mouseSensitivity;

    private static float mainVolume;
    private static float musicVolume;
    private static float soundVolume;


    private const float defaultMouseSensitivity = 25f;

    [Range(0.0001f, 1f)] private const float defaultVolume = 0.5f;


    private const string PLAYER_PREFS_MOUSE_SENSITIVITY = "mouseSensitivity";

    private const string PLAYER_PREFS_MAIN_VOLUME = "MainVolume";
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";


    public static float MouseSensitivity
    {
        get => mouseSensitivity;
        set
        {
            PlayerPrefs.SetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, value);
            mouseSensitivity = value;
        }
    }


    public static float MainVolume {
        get => mainVolume;
        set
        {
            PlayerPrefs.SetFloat(PLAYER_PREFS_MAIN_VOLUME, value);
            mainVolume = value;
        }
    }

    public static float MusicVolume
    {
        get => musicVolume;
        set
        {
            PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, value);
            musicVolume = value;
        }
    }

    public static float SoundVolume {
        get => soundVolume;
        set
        {
            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, value);
            soundVolume = value;
        }
    }


    public static void LoadSettings()
    {
        MouseSensitivity = PlayerPrefs.GetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, defaultMouseSensitivity);

        MainVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MAIN_VOLUME, defaultVolume);
        MusicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, defaultVolume);
        SoundVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, defaultVolume);
    }
}