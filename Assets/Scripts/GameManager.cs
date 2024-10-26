using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event Action<bool> OnGamePauseUnpause;

    public bool GamePaused { get; private set; }


    private void Awake() {
        Instance = this;

        GameSettings.ResetSettings();

        ShowCursor(false);

        GamePaused = false;
    }


    private void Start() {
        GameInput.Instance.OnPauseAction += () => { PauseGameToMenu(!GamePaused); };

        PauseGameToMenu(false);
    }

    public void PauseGame(bool targetState) {
        StartTime(!targetState);
        ShowCursor(targetState);
        GamePaused = targetState;
    }

    public void PauseGameToMenu(bool targetState) {
        OnGamePauseUnpause?.Invoke(targetState);
        PauseGame(targetState);
    }

    public void ShowCursor(bool targetState) {
        if (targetState) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = targetState;
    }


    public void StartTime(bool targetState) {
        if (targetState) SetTimeScale(1f);
        else SetTimeScale(0f);
    }


    public void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
    }
}
