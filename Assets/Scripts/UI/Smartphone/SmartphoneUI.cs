using UnityEngine;
using UnityEngine.UI;

public class SmartphoneUI : MonoBehaviour
{
    [SerializeField]
    private Animator smartphoneAnimator;

    private RectTransform smartphoneRectTransform;

    [SerializeField]
    private Vector2 defaultSmartphonePosition = new(640f, -800f);
    
    private bool isSmartphoneEnabled;

    private const string PHONE_ON_TRIGGER = "PhoneOn";
    private const string PHONE_OFF_TRIGGER = "PhoneOff";

    [SerializeField]
    private SmartphoneMenuUI mainMenu;

    [SerializeField]
    private Button mainMenuButton;

    private void Awake()
    {
        smartphoneRectTransform = smartphoneAnimator.GetComponent<RectTransform>();

        mainMenuButton.onClick.AddListener(() => mainMenu.GoToMainMenu());

        smartphoneRectTransform.localPosition = defaultSmartphonePosition;

        isSmartphoneEnabled = false;
    }

    private void OnEnable()
    {
        GameInput.OnSmartphoneToggleAction += EnableDisableSmartphone;
    }

    private void OnDisable()
    {
        GameInput.OnSmartphoneToggleAction -= EnableDisableSmartphone;
    }

    private void EnableDisableSmartphone()
    {
        SetSmartphoneEnabled(!isSmartphoneEnabled);
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

    public void SetCanShowSmartphone(bool canShow)
    {
        // May be a temporary solution.
        gameObject.SetActive(canShow);
    }
}
