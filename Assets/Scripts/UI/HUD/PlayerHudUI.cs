using UnityEngine;

public class PlayerHudUI : MonoBehaviour
{
    public bool CanShowPlayerHUD { get; set; }

    private void Start()
    {
        GameManager.Instance.GamePausedUnpaused += (gamePaused) => SetPlayerHUDEnabled(!gamePaused);

        SetPlayerHUDEnabled(!GameManager.Instance.IsGamePaused);
    }

    public void SetPlayerHUDEnabled(bool enabled)
    {
        // Enable/disable player HUD only if it is permitted
        gameObject.SetActive(enabled && CanShowPlayerHUD);
    }
}
