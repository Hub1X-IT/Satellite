using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneController : MonoBehaviour {

    [SerializeField] SmartphoneVisual smartphoneVisual;


    [SerializeField] private Vector2 defaultSmartphonePosition = new Vector2(640, -800);


    bool smartphoneOn = false;    


    private void Start() {
        GameInput.Instance.OnSmartphoneToggleAction += GameInput_OnSmartphoneToggleAction;

        Debug.Log(smartphoneVisual);
        Debug.Log(defaultSmartphonePosition);

        smartphoneVisual.SetPosition(defaultSmartphonePosition);
    }


    private void GameInput_OnSmartphoneToggleAction(object sender, System.EventArgs e) {
        SmartphoneToggle();
    }


    void SmartphoneToggle() {
        if (smartphoneOn == false) {
            TurnOn();
        }
        else {
            TurnOff();
        }
    }


    private void TurnOn() {
        smartphoneVisual.TurnOn();
        smartphoneOn = true;
        CrosshairController.Instance.HideCrosshair();
        CursorController.Instance.ShowCursor();
        TimeScaleController.Instance.StopTime();
    }


    private void TurnOff() {
        smartphoneVisual.TurnOff();
        smartphoneOn = false;
        CrosshairController.Instance.ShowCrosshair();
        CursorController.Instance.HideCursor();
        TimeScaleController.Instance.StartTime();
    }


    public Vector2 GetDefaultSmartphonePosition() {
        return defaultSmartphonePosition;
    }
}
