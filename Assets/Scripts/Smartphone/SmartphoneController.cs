using UnityEngine;

public class SmartphoneController : MonoBehaviour {

    [SerializeField] private Vector2 defaultSmartphonePosition = new Vector2(640, -800);

    bool smartphoneOn = false;

    [SerializeField] private Animator smartphoneAnimator;

    private RectTransform smartphoneRectTransform;

    private const string PHONE_ON_TRIGGER = "PhoneOn";
    private const string PHONE_OFF_TRIGGER = "PhoneOff";

    private void Awake() {
        smartphoneRectTransform = smartphoneAnimator.GetComponent<RectTransform>();

        SetPosition(defaultSmartphonePosition);
    }

    private void Start() {
        GameInput.Instance.OnSmartphoneToggleAction += () => { TurnOn(!smartphoneOn); };
    }

    private void TurnOn(bool targetState) {
        smartphoneOn = targetState;
        GameManager.Instance.PauseGame(targetState);

        if (targetState) smartphoneAnimator.SetTrigger(PHONE_ON_TRIGGER);
        else smartphoneAnimator.SetTrigger(PHONE_OFF_TRIGGER);
    }

    private void SetPosition(Vector2 position) {
        smartphoneRectTransform.localPosition = position;
    }
}
