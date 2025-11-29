using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<bool> GamePausedUnpaused;

    public bool IsGamePaused { get; private set; }

    public bool IsInScreenView { get; set; }

    public bool IsGuidebookOrSmartphoneEnabled { get; set; }

    public CursorLockMode HiddenCursorLockMode { get; set; }
    private CursorLockMode ShownCursorLockMode { get; set; } = CursorLockMode.None;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(GameManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        HiddenCursorLockMode = CursorLockMode.Locked;
        IsInScreenView = false;
        IsGuidebookOrSmartphoneEnabled = false;
    }

    private void Start()
    {
        VirtualClipboard.InitializeVirtualClipboard();

        GameInput.Instance.OnPauseAction += () => PauseGameToMenu(!IsGamePaused);
        PauseGameToMenu(false);
    }

    public void SetGamePaused(bool paused)
    {
        IsGamePaused = paused;
        SetTimeStarted(!paused);
        // May be temporary
        if (!IsInScreenView)
        {
            SetCursorShown(paused);
        }
        else
        {
            SetCursorShown(true);
        }
    }

    public void PauseGameToMenu(bool paused)
    {
        GamePausedUnpaused?.Invoke(paused);
        SetGamePaused(paused);
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

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}