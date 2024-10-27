using UnityEngine;

public class Screen : MonoBehaviour
{

    [SerializeField] private GameObject screen;


    private bool isTurnedOn;
    public bool IsTurnedOn
    {
        get => isTurnedOn;
        set
        {
            screen.SetActive(value);
            isTurnedOn = value;
        }
    }
}