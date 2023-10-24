using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    
    [SerializeField] private GameObject[] turnedOnObjects;
    [SerializeField] private GameObject[] turnedOffObjects;


    private bool isTurnedOn;
    private int onOffCycles;
    private int maxOnOffCycles = 5;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.F) && onOffCycles < maxOnOffCycles) {
            if (!isTurnedOn) {
                TurnOn();
            }
            else {
                TurnOff();
            }
        }
    }


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

        onOffCycles++;
    }
}