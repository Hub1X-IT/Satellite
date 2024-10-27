using System;
using UnityEngine;

public class GameManagerOld : MonoBehaviour
{
    public static GameManagerOld Instance { get; private set; }

    public float interactRange;

    [Tooltip("Only one should be selected!")] public LayerMask defaultInteractableLayerMask;
    [Tooltip("Select also the layers that interaction should not pass through")] public LayerMask interactableLayerMasks;

    public static event Action<bool> OnGamePauseUnpause;

    public static bool GamePaused { get; private set; }


    private void Awake()
    {
        Instance = this;
        GameInput.InitializeInput();

        GameSettingsManager.LoadSettings();

        ShowCursor(false);

        GamePaused = false;
    }


    private void Start()
    {
        GameInput.OnPauseAction += () => { PauseGameToMenu(!GamePaused); };

        PauseGameToMenu(false);
    }

    private void OnDestroy()
    {
        GameInput.RemoveInput();
    }

    public static void PauseGame(bool targetState)
    {
        StartTime(!targetState);
        ShowCursor(targetState);
        GamePaused = targetState;
    }

    public static void PauseGameToMenu(bool targetState)
    {
        OnGamePauseUnpause?.Invoke(targetState);
        PauseGame(targetState);
    }

    public static void ShowCursor(bool targetState)
    {
        if (targetState) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = targetState;
    }


    public static void StartTime(bool targetState)
    {
        if (targetState) SetTimeScale(1f);
        else SetTimeScale(0f);
    }


    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
