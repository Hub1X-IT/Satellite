using UnityEngine;

public class SmartphoneController : MonoBehaviour
{
    [SerializeField]
    private Animator smartphoneAnimator;

    private bool isSmartphoneEnabled;

    private const string IsPhoneOnParam = "IsPhoneOn";

    private void Awake()
    {
        isSmartphoneEnabled = false;
    }

    private void Start()
    {
        GameInput.Instance.OnSmartphoneEnableAction += TryEnableSmartphone;

        GameInput.Instance.OnSmartphoneDisableAction += TryDisableSmartphone;
    }

    private void TryEnableSmartphone()
    {
        if (gameObject.activeInHierarchy && !isSmartphoneEnabled && !GameManager.Instance.IsInScreenView)
        {
            SetSmartphoneEnabled(true);
        }
    }

    private void TryDisableSmartphone()
    {
        if (gameObject.activeInHierarchy && isSmartphoneEnabled)
        {
            SetSmartphoneEnabled(false);
        }
    }

    private void SetSmartphoneEnabled(bool enabled)
    {
        isSmartphoneEnabled = enabled;

        GameManager.Instance.IsInSmartphoneView = enabled;
        
        PlayerScriptsController.Instance.SetPlayerMovementEnabled(!enabled);
        PlayerScriptsController.Instance.SetInteractionEnabled(!enabled);
        PlayerScriptsController.Instance.SetCrosshairEnabled(!enabled);

        if (enabled)
        {
            GameInput.Instance.CurrentInputActions.PlayerWalking.Disable();
            GameInput.Instance.CurrentInputActions.Smartphone.Enable();
        }
        else
        {
            GameInput.Instance.CurrentInputActions.Smartphone.Disable();
            GameInput.Instance.CurrentInputActions.PlayerWalking.Enable();
        }

        GameManager.Instance.SetCursorShown(enabled);

        smartphoneAnimator.SetBool(IsPhoneOnParam, enabled);

        PlayerScriptsController.Instance.UpdateFlashlightState();
    }

    public void SetCanShowSmartphone(bool canShow)
    {
        // May be a temporary solution.
        gameObject.SetActive(canShow);
    }
}
