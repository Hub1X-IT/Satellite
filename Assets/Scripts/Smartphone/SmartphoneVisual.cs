using UnityEngine;

public class SmartphoneVisual : MonoBehaviour
{
    private Animator smartphoneAnimator;


    private RectTransform smartphoneTransform;


    private const string PHONE_ON_TRIGGER = "PhoneOn";
    private const string PHONE_OFF_TRIGGER = "PhoneOff";


    private void Awake() {
        smartphoneAnimator = GetComponent<Animator>();
        smartphoneTransform = GetComponent<RectTransform>();
    }


    public void TurnOn(bool targetState) {
        if (targetState) smartphoneAnimator.SetTrigger(PHONE_ON_TRIGGER);
        else smartphoneAnimator.SetTrigger(PHONE_OFF_TRIGGER);
    }


    public void SetPosition(Vector2 position) {
        smartphoneTransform.localPosition = position;
    }
}
