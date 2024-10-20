using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public bool GamePaused { get; private set; }

    [SerializeField] CanvasRenderer crosshair; // probably should be moved to a script


    private void Awake() {
        Instance = this;

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
        crosshair.gameObject.SetActive(targetState);
    }
}
