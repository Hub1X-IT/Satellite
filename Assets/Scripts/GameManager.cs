using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event Action<bool> OnGamePauseUnpause;

    public bool GamePaused { get; private set; }

    private CrosshairController crosshairController;


    private void Awake() {
        Instance = this;
        
        crosshairController = FindAnyObjectByType<CrosshairController>(); // there should be only one CrosshairController in the scene

        GameSettings.ResetSettings();

        ShowCursor(false);
    }


    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;


    }

    private void GameInput_OnPauseAction() {
        OnGamePauseUnpause?.Invoke(!GamePaused);
        PauseGame(!GamePaused);
    }

    public void PauseGame(bool targetState) {
        StartTime(!targetState);
        ShowCursor(targetState);
        ShowCrosshair(!targetState);
        GamePaused = targetState;
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


    public void ShowCrosshair(bool targetState) {
        crosshairController.ShowCrosshair(targetState);
    }
}
