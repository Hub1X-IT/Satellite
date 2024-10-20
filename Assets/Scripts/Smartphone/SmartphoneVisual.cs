using UnityEngine;

public class SmartphoneVisual : MonoBehaviour
{
    private Animator smartphoneAnimator;


    private RectTransform smartphoneTransform;


    private const string PHONE_ON = "PhoneOn";
    private const string PHONE_OFF = "PhoneOff";


    private void Awake() {
        smartphoneAnimator = GetComponent<Animator>();
        smartphoneTransform = GetComponent<RectTransform>();
    }


    public void TurnOn() {
        smartphoneAnimator.SetTrigger(PHONE_ON);
    }


    public void TurnOff() {
        smartphoneAnimator.SetTrigger(PHONE_OFF);
    }


    public void SetPosition(Vector2 position) {
        smartphoneTransform.localPosition = position;
    }
}
