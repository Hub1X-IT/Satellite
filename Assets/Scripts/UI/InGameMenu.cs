using System;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public event Action<bool> OptionsOpenedClosed;


    private bool areOptionsOpen;


    public bool AreOptionsOpen
    {
        get => areOptionsOpen;
        set
        {
            // Open/close options
            areOptionsOpen = value;
            OptionsOpenedClosed?.Invoke(value);
        }
    }


    private void Awake()
    {
        GameManager.GamePausedUnpaused += (state) => gameObject.SetActive(state);
    }


    private void OnEnable()
    {
        // The options need to be closed when pausing.
        AreOptionsOpen = true;
    }
}