using System;
using UnityEngine;

public static class GameManager
{
    public static event Action<bool> GamePausedUnpaused;


    public static bool IsGamePaused { get; private set; }

    public static bool IsCursorShown { get; private set; }

    public static bool IsTimeStarted { get; private set; }

    public static float TimeScale { get; private set; }


    public static void InitializeOnAwake()
    {
        GameInput.OnPauseAction += () => PauseGameToMenu(!IsGamePaused);
    }

    public static void InitializeOnStart()
    {
        SetCursorShown(false);
        PauseGameToMenu(false);
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
        IsCursorShown = shown;
        if (shown) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = shown;
    }

    public static void SetTimeStarted(bool started)
    {
        IsTimeStarted = started;
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
        TimeScale = timeScale;
        Time.timeScale = timeScale;
    }
}