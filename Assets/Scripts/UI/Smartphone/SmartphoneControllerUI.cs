using UnityEngine;

public class SmartphoneControllerUI : MonoBehaviour
{
    [SerializeField]
    private Animator smartphoneAnimator;

    private RectTransform smartphoneRectTransform;

    [SerializeField]
    private Vector2 defaultSmartphonePosition = new(640, -800);
    
    private const string PHONE_ON_TRIGGER = "PhoneOn";
    private const string PHONE_OFF_TRIGGER = "PhoneOff";


    public bool IsSmartphoneEnabled { get; private set; }


    private void Awake()
    {
        smartphoneRectTransform = smartphoneAnimator.GetComponent<RectTransform>();

        GameInput.OnSmartphoneToggleAction += () => SetSmartphoneEnabled(!IsSmartphoneEnabled);

        smartphoneRectTransform.localPosition = defaultSmartphonePosition;

        IsSmartphoneEnabled = false;
    }


    private void SetSmartphoneEnabled(bool enabled)
    {
        IsSmartphoneEnabled = enabled;

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
