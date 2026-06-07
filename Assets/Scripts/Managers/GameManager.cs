using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<bool> GamePausedUnpaused;

    public bool IsGamePaused { get; private set; }

    public bool IsInScreenView { get; set; }

    public bool IsInSmartphoneView { get; set; }

    private const CursorLockMode HiddenCursorLockMode = CursorLockMode.Locked;
    private const CursorLockMode ShownCursorLockMode = CursorLockMode.None;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(GameManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        IsInScreenView = false;
        IsInSmartphoneView = false;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += () => PauseGameToMenu(!IsGamePaused);
        PauseGameToMenu(false);
    }

    public void PauseGameToMenu(bool paused)
    {
        GamePausedUnpaused?.Invoke(paused);
        SetGamePaused(paused);
        ToggleActionMaps(paused);
    }

    private void SetGamePaused(bool paused)
    {
        IsGamePaused = paused;
        SetTimeStarted(!paused);
        if (IsInScreenView || IsInSmartphoneView)
        {
            SetCursorShown(true);
        }
        else
        {
            SetCursorShown(paused);
        }
    }

    private void ToggleActionMaps(bool isPaused)
    {
        if (isPaused)
        {
            GameInput.Instance.CurrentInputActions.PlayerWalking.Disable();
            GameInput.Instance.CurrentInputActions.Smartphone.Disable();
        }
        else
        {
            if (IsInSmartphoneView)
            {
                GameInput.Instance.CurrentInputActions.Smartphone.Enable();
            }
            else
            {
                GameInput.Instance.CurrentInputActions.PlayerWalking.Enable();
            }
        }
    }

    public void SetCursorShown(bool shown)
    {
        Cursor.lockState = shown ? ShownCursorLockMode : HiddenCursorLockMode;
        Cursor.visible = shown;
    }

    public void SetTimeStarted(bool started)
    {
        SetTimeScale(started ? 1f : 0f);
    }

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}