using UnityEngine;

public class PlayerHudUI : MonoBehaviour
{
    public bool CanShowPlayerHUD { get; set; }

    private void Awake()
    {
        GameManager.GamePausedUnpaused += (gamePaused) => SetPlayerHUDEnabled(!gamePaused);
    }

    public void SetPlayerHUDEnabled(bool enabled)
    {
        // Enable/disable player HUD only if it is permitted
        gameObject.SetActive(enabled && CanShowPlayerHUD);
    }
}
