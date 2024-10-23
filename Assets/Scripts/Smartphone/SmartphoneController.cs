using UnityEngine;

public class SmartphoneController : MonoBehaviour {

    private SmartphoneVisual smartphoneVisual;


    [SerializeField] private Vector2 defaultSmartphonePosition = new Vector2(640, -800);


    bool smartphoneOn = false;

    private void Awake() {
        smartphoneVisual = GetComponentInChildren<SmartphoneVisual>();
    }

    private void Start() {
        GameInput.Instance.OnSmartphoneToggleAction += GameInput_OnSmartphoneToggleAction;

        smartphoneVisual.SetPosition(defaultSmartphonePosition);
    }


    private void GameInput_OnSmartphoneToggleAction(object sender, System.EventArgs e) {
        TurnOn(!smartphoneOn);
    }


    private void TurnOn(bool targetState) {
        smartphoneVisual.TurnOn(targetState);
        smartphoneOn = targetState;
        GameManager.Instance.PauseGame(targetState);        
    }
}
