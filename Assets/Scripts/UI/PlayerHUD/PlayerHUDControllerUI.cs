using UnityEngine;

public class PlayerHUDControllerUI : MonoBehaviour
{
    public bool CanShowPlayerHUD { get; private set; }

    public bool IsEnabled { get; private set; }


    private void Awake()
    {
        GameManager.GamePausedUnpaused += (gamePaused) => SetPlayerHUDEnabled(!gamePaused);
    }

    private void SetPlayerHUDEnabled(bool enabled)
    {
        // Enable/disable player HUD only if it is allowed
        enabled = enabled && CanShowPlayerHUD;

        IsEnabled = enabled;
        gameObject.SetActive(enabled);
    }

    public void SetCanShowPlayerHUD(bool canShow)
    {
        CanShowPlayerHUD = canShow;
        SetPlayerHUDEnabled(!GameManager.IsGamePaused);
    }
}
