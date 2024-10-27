using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{

    private bool canShowPlayerHUD;

    private bool isEnabled;


    public bool CanShowPlayerHUD
    {
        get => canShowPlayerHUD;
        set
        {
            canShowPlayerHUD = value;
            IsEnabled = !GameManager.IsGamePaused;
        }
    }

    private bool IsEnabled
    {
        get => isEnabled;
        set
        {
            // Enable/disable player HUD if it is allowed
            value = value && CanShowPlayerHUD;
            gameObject.SetActive(value);
            isEnabled = value;
        }
    }


    private void Awake()
    {
        GameManager.GamePausedUnpaused += (state) => IsEnabled = !state;
    }
}
