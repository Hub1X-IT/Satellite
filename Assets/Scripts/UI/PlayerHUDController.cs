using UnityEngine;

public class PlayerHUDController : MonoBehaviour {

    private bool canShowPlayerHUD;

    private void Awake() {
        GameManager.OnGamePauseUnpause += PauseUnpause;
    }

    private void PauseUnpause(bool paused) {
        gameObject.SetActive(!paused && canShowPlayerHUD);
    }

    public void CanShowPlayerHUD(bool targetState) {
        canShowPlayerHUD = targetState;
        PauseUnpause(GameManager.GamePaused);
    }
}
