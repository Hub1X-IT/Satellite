using UnityEngine;

public class GameSettingsManager : MonoBehaviour {

    public static float MouseSensitivity { get; private set; }
    private const float defaultMouseSensitivity = 25f;
    private const string PLAYER_PREFS_MOUSE_SENSITIVITY = "mouseSensitivity";

    public static float MainVolume { get; private set; }
    private const string PLAYER_PREFS_MAIN_VOLUME = "MainVolume";

    public static float MusicVolume { get; private set; }
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static float SoundVolume { get; private set; }
    private const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";

    private const float defaultVolume = 0.5f;

    public static void LoadSettings() {
        MouseSensitivity = PlayerPrefs.GetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, defaultMouseSensitivity);

        MainVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MAIN_VOLUME, defaultVolume);
        MusicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, defaultVolume);
        SoundVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, defaultVolume);
    }

    public static void SetMouseSensitivity(float value) { MouseSensitivity = value; PlayerPrefs.SetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, value); }

    public static void SetMainVolume(float value) { MainVolume = value; PlayerPrefs.SetFloat(PLAYER_PREFS_MAIN_VOLUME, value); }

    public static void SetMusicVolume(float value) { MusicVolume = value; PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, value); }

    public static void SetSoundVolume(float value) { SoundVolume = value; PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, value); }
}