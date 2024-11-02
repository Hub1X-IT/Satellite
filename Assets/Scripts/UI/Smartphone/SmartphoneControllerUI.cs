using UnityEngine;

public class SmartphoneControllerUI : MonoBehaviour
{
    [SerializeField]
    private Animator smartphoneAnimator;

    private RectTransform smartphoneRectTransform;

    [SerializeField]
    private Vector2 defaultSmartphonePosition = new(640f, -800f);
    
    private bool isSmartphoneEnabled;

    private const string PHONE_ON_TRIGGER = "PhoneOn";
    private const string PHONE_OFF_TRIGGER = "PhoneOff";

    private void Awake()
    {
        smartphoneRectTransform = smartphoneAnimator.GetComponent<RectTransform>();

        GameInput.OnSmartphoneToggleAction += () => SetSmartphoneEnabled(!isSmartphoneEnabled);

        smartphoneRectTransform.localPosition = defaultSmartphonePosition;

        isSmartphoneEnabled = false;
    }

    private void SetSmartphoneEnabled(bool enabled)
    {
        isSmartphoneEnabled = enabled;

        GameManager.SetGamePaused(enabled);

        if (enabled)
        {
            smartphoneAnimator.SetTrigger(PHONE_ON_TRIGGER);
        }
        else
        {
            smartphoneAnimator.SetTrigger(PHONE_OFF_TRIGGER);
        }
    }
}
