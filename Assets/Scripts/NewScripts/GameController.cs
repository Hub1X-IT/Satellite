using System;
using UnityEngine;

public class GameController {
    // Game manager

    public static event Action<bool> OnGamePauseUnpause;

    public static bool GamePaused { get; private set; }


    public static void InitializeOnAwake() {
        ShowCursor(false);

        GamePaused = false;
    }

    public static void InitializeOnStart() {
        GameInput.OnPauseAction += () => { PauseGameToMenu(!GamePaused); };

        PauseGameToMenu(false);
    }

    public static void PauseGame(bool targetState) {
        StartTime(!targetState);
        ShowCursor(targetState);
        GamePaused = targetState;
    }

    public static void PauseGameToMenu(bool targetState) {
        OnGamePauseUnpause?.Invoke(targetState);
        PauseGame(targetState);
    }

    public static void ShowCursor(bool targetState) {
        if (targetState) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = targetState;
    }


    public static void StartTime(bool targetState) {
        if (targetState) SetTimeScale(1f);
        else SetTimeScale(0f);
    }


    public static void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
    }
}