using System;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance { get; private set; }

    public event Action<bool> OnPowerStateChanged;

    [SerializeField]
    private bool defaultPowerState = true;

    private bool isPowerOn;

    public bool IsPowerOn => isPowerOn;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(PowerManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        isPowerOn = defaultPowerState;
    }

    public void SetPowerState(bool isOn)
    {
        if (isPowerOn != isOn)
        {
            isPowerOn = isOn;
            OnPowerStateChanged?.Invoke(isPowerOn);
        }
    }
}