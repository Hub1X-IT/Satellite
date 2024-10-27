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
            IsTimeStarted = !value;
            IsCursorShown = value;

            isGamePaused = value;
        }
    }

    public static bool IsCursorShown
    {
        get => isCursorShown;
        set
        {
            // Show or hide cursor.
            if (value) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = value;

            isCursorShown = value;
        }
    }

    public static bool IsTimeStarted
    {
        get => isTimeStarted;
        set
        {
            // Start or stop time.
            if (value) TimeScale = 1f;
            else TimeScale = 0f;

            isTimeStarted = value;
        }
    }

    public static float TimeScale
    {
        get => timeScale;
        set
        {
            // Set time scale
            Time.timeScale = value;

            timeScale = value;
        }
    }

    public static void InitializeOnStart()
    {
        GameInput.OnPauseAction += () => { PauseGameToMenu(!IsGamePaused); };

        IsCursorShown = false;
        PauseGameToMenu(false);
    }

    public static void PauseGameToMenu(bool targetState)
    {
        GamePausedUnpaused?.Invoke(targetState);
        IsGamePaused = targetState;
    }
}