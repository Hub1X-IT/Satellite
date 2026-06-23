using UnityEngine;

public class PlayerHudUI : MonoBehaviour
{
    public bool CanShowPlayerHUD { get; set; }

    [SerializeField]
    private GameObject crosshair;

    [SerializeField]
    private GameObject flashlightInfo;

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

    public void SetCrosshairEnabled(bool enabled)
    {
        crosshair.SetActive(enabled);
    }

    public void SetFlashlightInfoEnabled(bool enabled)
    {
        flashlightInfo.SetActive(enabled);
    }
}
