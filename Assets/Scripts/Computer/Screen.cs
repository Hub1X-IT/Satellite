using UnityEngine;

public class Screen : MonoBehaviour {

    [SerializeField] private GameObject screen;

    private bool isTurnedOn;


    public void TurnOn() {
        isTurnedOn = true;
        screen.SetActive(true);
    }
    
    public void TurnOff() {
        isTurnedOn = false;
        screen.SetActive(false);
    }

    public bool IsTurnedOn() {
        return isTurnedOn;
    }
}