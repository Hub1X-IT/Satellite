using UnityEngine;

public class PlayerHudUI : MonoBehaviour
{
    [SerializeField]
    private GameObject crosshair;

    [SerializeField]
    private GameObject flashlightInfo;

    [SerializeField]
    private GameObject FPHUD;

    [SerializeField]
    private GameObject objectivesHUD;

    private void Start()
    {
        GameManager.Instance.GamePausedUnpaused += (gamePaused) => SetPlayerHUDEnabled(!gamePaused);

        SetPlayerHUDEnabled(!GameManager.Instance.IsGamePaused);
    }

    public void SetPlayerHUDEnabled(bool enabled)
    {
        // Enable/disable player HUD only if it is permitted
        gameObject.SetActive(enabled);
    }

    public void SetPlayerFPHUDEnabled(bool enabled){
        FPHUD.SetActive(enabled);
    }

    public void SetPlayerObjectivesHUDEnabled(bool enabled){
        objectivesHUD.SetActive(enabled);
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
