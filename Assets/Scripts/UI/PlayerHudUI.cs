using UnityEngine;

public class PlayerHudUI : MonoBehaviour
{
    private bool canShowPlayerHUD;

    private void Awake()
    {
        GameManager.GamePausedUnpaused += (gamePaused) => SetPlayerHUDEnabled(!gamePaused);
    }

    private void SetPlayerHUDEnabled(bool enabled)
    {
        // Enable/disable player HUD only if it is permitted
        gameObject.SetActive(enabled && canShowPlayerHUD);
    }

    public void SetCanShowPlayerHUD(bool canShow)
    {
        canShowPlayerHUD = canShow;
        SetPlayerHUDEnabled(!GameManager.IsGamePaused);
    }
}
