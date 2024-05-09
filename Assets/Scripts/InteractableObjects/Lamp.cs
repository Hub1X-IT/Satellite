using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable {

    
    [SerializeField] private GameObject[] turnedOnObjects;
    [SerializeField] private GameObject[] turnedOffObjects;


    private bool isTurnedOn;
    // private int onOffCycles;
    // private int maxOnOffCycles = 5;


    private void TurnOn() {
        Debug.Log("TurnOn");

        isTurnedOn = true;

        foreach (GameObject obj in turnedOnObjects) {
            obj.SetActive(true);
        }
        foreach (GameObject obj in turnedOffObjects) {
            obj.SetActive(false);
        }
    }

    private void TurnOff() {
        Debug.Log("TurnOff");

        isTurnedOn = false;
        foreach (GameObject obj in turnedOnObjects) {
            obj.SetActive(true);
        }

        foreach (GameObject obj in turnedOnObjects) {
            obj.SetActive(false);
        }

        // onOffCycles++;
    }

    public void Interact() {
        if (!isTurnedOn) {
            TurnOn();
        }
        else {
            TurnOff();
        }
    }

    public Transform GetTransform() {
        return transform;
    }
}