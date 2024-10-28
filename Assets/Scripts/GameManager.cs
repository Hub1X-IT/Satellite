using System;
using UnityEngine;

public static class GameManager
{
    public static event Action<bool> GamePausedUnpaused;


    private static bool isGamePaused;

    private static bool isCursorShown;

    private static bool isTimeStarted;

    private static float timeScale;


    public static bool IsGamePaused
    {
        get => isGamePaused;
        set
        {
            // Pause or unpause game.
            isGamePaused = value;

            IsTimeStarted = !value;
            IsCursorShown = value;
        }
    }

    public static bool IsCursorShown
    {
        get => isCursorShown;
        set
        {
            // Show or hide cursor.
            isCursorShown = value;

            if (value) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = value;
        }
    }

    public static bool IsTimeStarted
    {
        get => isTimeStarted;
        set
        {
            // Start or stop time.
            isTimeStarted = value;
            
            if (value) TimeScale = 1f;
            else TimeScale = 0f;
        }
    }

    public static float TimeScale
    {
        get => timeScale;
        set
        {
            // Set time scale
            timeScale = value;

            Time.timeScale = value;
        }
    }

    public static void InitializeOnAwake()
    {
        GameInput.OnPauseAction += () => PauseGameToMenu(!IsGamePaused);
    }

    public static void InitializeOnStart()
    {
        IsCursorShown = false;
        PauseGameToMenu(false);
    }

    public static void PauseGameToMenu(bool targetState)
    {
        GamePausedUnpaused?.Invoke(targetState);
        IsGamePaused = targetState;
    }
}