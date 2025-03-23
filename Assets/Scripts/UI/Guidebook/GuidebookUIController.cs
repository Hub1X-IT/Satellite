using UnityEngine;

public class GuidebookUIController : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    private Vector2 guidebookEnabledPosition = Vector2.zero;

    [SerializeField]
    private Vector2 guidebookDisabledPosition = new(1580f, 0f);

    private bool isGuidebookEnabled;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.localPosition = guidebookDisabledPosition;

        isGuidebookEnabled = false;
    }

    private void OnEnable()
    {
        GameInput.OnGuidebookToggleAction += TryToggleGuidebook;
    }

    private void OnDisable()
    {
        GameInput.OnGuidebookToggleAction -= TryToggleGuidebook;
    }

    private void TryToggleGuidebook()
    {
        // Check all the necessary conditions here.
        if (isGuidebookEnabled)
        {
            SetGuidebookEnabled(false);
        }
        else if (!isGuidebookEnabled && !GameManager.IsInScreenView && !GameManager.IsGuidebookOrSmartphoneEnabled)
        {
            SetGuidebookEnabled(true);
        }
    }

    private void SetGuidebookEnabled(bool enabled)
    {
        isGuidebookEnabled = enabled;

        rectTransform.localPosition = enabled ? guidebookEnabledPosition : guidebookDisabledPosition;

        GameManager.IsGuidebookOrSmartphoneEnabled = enabled;
        GameManager.SetGamePaused(enabled);

        if (enabled)
        {
            GameInput.PlayerInputActions.Guidebook.Enable();
        }
        else
        {
            GameInput.PlayerInputActions.Guidebook.Disable();
        }
    }
}
