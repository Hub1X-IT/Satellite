using UnityEngine;

public class PlayerHUDControllerUI : MonoBehaviour
{
    private bool canShowPlayerHUD;

    public bool IsEnabled { get; private set; }


    private void Awake()
    {
        GameManager.GamePausedUnpaused += (gamePaused) => SetPlayerHUDEnabled(!gamePaused);
    }

    private void SetPlayerHUDEnabled(bool enabled)
    {
        // Enable/disable player HUD only if it is permitted
        enabled = enabled && canShowPlayerHUD;

        IsEnabled = enabled;
        gameObject.SetActive(enabled);
    }

    public void SetCanShowPlayerHUD(bool canShow)
    {
        canShowPlayerHUD = canShow;
        SetPlayerHUDEnabled(!GameManager.IsGamePaused);
    }
}
