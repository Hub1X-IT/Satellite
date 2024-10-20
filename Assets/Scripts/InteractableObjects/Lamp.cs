using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable {

    
    [SerializeField] private GameObject[] turnedOnObjects;
    [SerializeField] private GameObject[] turnedOffObjects;


    private bool isTurnedOn;
    // private int onOffCycles;
    // private int maxOnOffCycles = 5;

    private void TurnOnOff(bool targetState) {
        Debug.Log("Lamp: TurnOnOff(), TargetState: " + targetState);

        isTurnedOn = targetState;

        foreach (GameObject obj in turnedOnObjects) {
            obj.SetActive(targetState);
        }
        foreach (GameObject obj in turnedOffObjects) {
            obj.SetActive(!targetState);
        }

        // if (!targetState) onOffCycles++;
    }
        
    public void Interact() {
        TurnOnOff(!isTurnedOn);
    }

    public Transform GetTransform() { return transform; }
}