using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public bool GamePaused { get; private set; }

    private CrosshairController crosshairController;


    private void Awake() {
        Instance = this;
        
        crosshairController = FindAnyObjectByType<CrosshairController>(); // there should be only one CrosshairController in the scene

        ShowCursor(false);
    }


    private void Start() {
        GameSettings.ResetSettings();
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
