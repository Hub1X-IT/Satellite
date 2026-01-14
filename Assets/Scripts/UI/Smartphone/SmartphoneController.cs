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
        GameInput.Instance.OnSmartphoneToggleAction += () =>
        {
            Debug.Log($"Smartphone enabled: {isSmartphoneEnabled}");
            if (gameObject.activeInHierarchy)
            {
                TryToggleSmartphone();
            }
        };
    }

    private void TryToggleSmartphone()
    {
        if (isSmartphoneEnabled)
        {
            SetSmartphoneEnabled(false);
        }
        else if (!isSmartphoneEnabled && !GameManager.Instance.IsInScreenView && !GameManager.Instance.IsGuidebookOrSmartphoneEnabled)
        {
            SetSmartphoneEnabled(true);
        }
    }

    private void SetSmartphoneEnabled(bool enabled)
    {
        isSmartphoneEnabled = enabled;

        GameManager.Instance.IsGuidebookOrSmartphoneEnabled = enabled;
        
        PlayerScriptsController.Instance.SetPlayerMovementEnabled(!enabled);
        PlayerScriptsController.Instance.SetInteractionEnabled(!enabled);

        GameManager.Instance.SetCursorShown(enabled);

        smartphoneAnimator.SetBool(IsPhoneOnParam, enabled);
    }

    public void SetCanShowSmartphone(bool canShow)
    {
        // May be a temporary solution.
        gameObject.SetActive(canShow);
    }
}
