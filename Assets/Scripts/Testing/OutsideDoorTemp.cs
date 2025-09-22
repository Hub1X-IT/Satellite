using UnityEngine;

public class OutsideDoorTemp : MonoBehaviour
{
    void Awake()
    {
        GetComponent<InteractionTrigger>().InteractVisual = GetComponent<InteractionVisual>();
    }
}