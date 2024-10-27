using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; set; }

    public Transform Transform { get; set; }

    private bool isTurnedOn;
    private bool IsTurnedOn
    {
        get => isTurnedOn;
        set
        {
            Debug.Log("Lamp: TurnOnOff(), TargetState: " + value);
                        

            foreach (GameObject obj in turnedOnObjects)
            {
                obj.SetActive(value);
            }
            foreach (GameObject obj in turnedOffObjects)
            {
                obj.SetActive(!value);
            }

            // if (!targetState) onOffCycles++;

            isTurnedOn = value;
        }
    }

    [SerializeField]
    private GameObject[] turnedOnObjects;

    [SerializeField]
    private GameObject[] turnedOffObjects;

    // private int onOffCycles;
    // private int maxOnOffCycles = 5;

    private void Awake()
    {
        Transform = transform;
    }

    public void Interact()
    {
        IsTurnedOn = !IsTurnedOn;
    }
}