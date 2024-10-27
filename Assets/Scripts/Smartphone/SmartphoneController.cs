using UnityEngine;

public class SmartphoneController : MonoBehaviour
{
    [SerializeField] private Animator smartphoneAnimator;


    private RectTransform smartphoneRectTransform;


    [SerializeField] private Vector2 defaultSmartphonePosition = new Vector2(640, -800);


    private Vector2 offPosition;
    

    private bool isSmartphoneOn;
    

    private const string PHONE_ON_TRIGGER = "PhoneOn";
    private const string PHONE_OFF_TRIGGER = "PhoneOff";


    private Vector2 OffPosition
    {
        get => offPosition;
        set
        {
            smartphoneRectTransform.localPosition = value;
            offPosition = value;
        }
    }


    private bool IsSmartphoneOn
    {
        get => isSmartphoneOn;
        set
        {
            // Turn smartphone on/off.
            GameManager.IsGamePaused = value;

            if (value) smartphoneAnimator.SetTrigger(PHONE_ON_TRIGGER);
            else smartphoneAnimator.SetTrigger(PHONE_OFF_TRIGGER);

            isSmartphoneOn = value;
        }
    }


    private void Awake()
    {
        smartphoneRectTransform = smartphoneAnimator.GetComponent<RectTransform>();

        OffPosition = defaultSmartphonePosition;
        isSmartphoneOn = false;
    }


    private void Start()
    {
        GameInput.OnSmartphoneToggleAction += () => IsSmartphoneOn = !IsSmartphoneOn;
    }
}
