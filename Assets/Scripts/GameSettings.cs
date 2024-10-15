using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    public static float MouseSensitivity { get; private set; }
    private const float defaultMouseSensitivity = 25f;
    private const string PLAYER_PREFS_MOUSE_SENSITIVITY = "mouseSensitivity";

    public static void SettingsReset() {
        MouseSensitivity = PlayerPrefs.GetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, defaultMouseSensitivity);
    }

    public static void SetMouseSensitivity(float value) {
        MouseSensitivity = value;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MOUSE_SENSITIVITY, value);
    }

}