using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneVisual : MonoBehaviour
{
    private Animator smartphoneAnimator;


    private Image smartphoneImage;


    private const string PHONE_ON = "PhoneOn";
    private const string PHONE_OFF = "PhoneOff";


    private void Awake() {
        smartphoneAnimator = GetComponent<Animator>();
        smartphoneImage = GetComponent<Image>();
    }


    public void TurnOn() {
        smartphoneAnimator.SetTrigger(PHONE_ON);
    }


    public void TurnOff() {
        smartphoneAnimator.SetTrigger(PHONE_OFF);
    }


    public void SetPosition(Vector2 position) {
        smartphoneImage.rectTransform.localPosition = position;
    }
}
