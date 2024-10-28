using UnityEngine;

public class SmartphoneController : MonoBehaviour
{
    [SerializeField]
    private Animator smartphoneAnimator;

    private RectTransform smartphoneRectTransform;

    [SerializeField]
    private Vector2 defaultSmartphonePosition = new Vector2(640, -800);

    private Vector2 smartphoneOffPosition;
    
    private bool isSmartphoneEnabled;
    
    private const string PHONE_ON_TRIGGER = "PhoneOn";
    private const string PHONE_OFF_TRIGGER = "PhoneOff";


    private Vector2 SmartphoneOffPosition
    {
        set
        {
            // Set smartphone off position
            smartphoneOffPosition = value;
            if (!IsSmartphoneEnabled)
            {
                smartphoneRectTransform.localPosition = value;
            }
        }
    }


    private bool IsSmartphoneEnabled
    {
        get => isSmartphoneEnabled;
        set
        {
            // Turn smartphone on/off.
            isSmartphoneEnabled = value;
            EnableDisableSmartphone(value);
        }
    }


    private void Awake()
    {
        smartphoneRectTransform = smartphoneAnimator.GetComponent<RectTransform>();

        GameInput.OnSmartphoneToggleAction += () => IsSmartphoneEnabled = !IsSmartphoneEnabled;

        SmartphoneOffPosition = defaultSmartphonePosition;
        isSmartphoneEnabled = false;
    }


    private void EnableDisableSmartphone(bool state)
    {
        GameManager.IsGamePaused = state;

        if (state)
        {
            smartphoneAnimator.SetTrigger(PHONE_ON_TRIGGER);
        }
        else
        {
            smartphoneAnimator.SetTrigger(PHONE_OFF_TRIGGER);
        }
    }
}
