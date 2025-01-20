using System;
using UnityEngine;

public static class GameManager
{
    public static event Action<bool> GamePausedUnpaused;

    public static bool IsGamePaused { get; private set; }

    public static bool IsInScreenView { get; set; }

    public static CursorLockMode HiddenCursorLockMode { get; set; }
    private static CursorLockMode ShownCursorLockMode { get; set; } = CursorLockMode.None;

    public static void OnAwake()
    {
        GameInput.OnPauseAction += () => PauseGameToMenu(!IsGamePaused);
        HiddenCursorLockMode = CursorLockMode.Locked;
        IsInScreenView = false;
    }

    public static void OnStart()
    {
        PauseGameToMenu(false);
    }

    public static void OnSceneExit()
    {
        GamePausedUnpaused = null;
    }

    public static void SetGamePaused(bool paused)
    {
        IsGamePaused = paused;
        SetTimeStarted(!paused);
        SetCursorShown(paused);
    }

    public static void PauseGameToMenu(bool paused)
    {
        GamePausedUnpaused?.Invoke(paused);
        SetGamePaused(paused);
    }

    public static void SetCursorShown(bool shown)
    {
        Cursor.lockState = shown ? ShownCursorLockMode : HiddenCursorLockMode;
        Cursor.visible = shown;
    }

    public static void SetTimeStarted(bool started)
    {
        SetTimeScale(started ? 1f : 0f);
    }

    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}