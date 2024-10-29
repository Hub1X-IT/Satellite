using System;
using UnityEngine;

public class InGameMenuUI : MonoBehaviour
{
    public event Action<bool> OptionsEnabled;

    public bool AreOptionsEnabled { get; private set; }

    private void Awake()
    {
        GameManager.GamePausedUnpaused += (state) => gameObject.SetActive(state);
    }

    private void OnEnable()
    {
        // Options need to be closed when pausing.
        SetOptionsEnabled(false);
    }


    public void SetOptionsEnabled(bool enabled)
    {
        AreOptionsEnabled = enabled;
        OptionsEnabled?.Invoke(enabled);
    }
}