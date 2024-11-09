using System;
using UnityEngine;

public static class GameManager
{
    public static event Action<bool> GamePausedUnpaused;

    public static bool IsGamePaused { get; private set; }

    public static void OnAwake()
    {
        GameInput.OnPauseAction += () => PauseGameToMenu(!IsGamePaused);
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
        if (shown) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = shown;
    }

    public static void SetTimeStarted(bool started)
    {
        if (started)
        {
            SetTimeScale(1f);
        }
        else
        {
            SetTimeScale(0f);
        }
    }

    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}