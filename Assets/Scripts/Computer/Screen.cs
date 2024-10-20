using UnityEngine;

public class Screen : MonoBehaviour {

    [SerializeField] private GameObject screen;

    private bool isTurnedOn;


    public void TurnOn(bool targetState) {
        isTurnedOn = targetState;
        screen.SetActive(targetState);
    }
}