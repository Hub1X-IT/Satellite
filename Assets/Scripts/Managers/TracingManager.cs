using System;
using UnityEngine;

public class TracingManager : MonoBehaviour
{
    public static TracingManager Instance { get; private set; }

    public event Action OnTracingStarted;
    public event Action OnPlayerTraced;

    private float tracingSpeedMultiplier = 1f;
    private int currentTracingSpeedLevel;

    private const float DefaultTracingTime = 60f;
    // private const float DefaultTracingTime = 5f; // For debugging

    private static readonly float[] tracingSpeedLevels = { 1f, 1.1f, 1.2f }; // to be adjusted later

    public bool IsTracingActive { get; private set; }

    public float TracingTimer { get; private set; }

    public float TracingProgress => (DefaultTracingTime - TracingTimer) / DefaultTracingTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(TracingManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        PowerManager.Instance.OnPowerStateChanged += (isPowerEnabled) =>
        {
            if (!isPowerEnabled)
            {
                InterruptTracing();
            }
        };
        ServerConnectionManager.Instance.OnServerConnectionStateChanged += (isConnected) =>
        {
            if (!isConnected)
            {
                InterruptTracing();
            }
        };
    }

    private void Update()
    {
        if (IsTracingActive)
        {
            TracingTimer -= Time.deltaTime * tracingSpeedMultiplier;
            if (TracingTimer <= 0)
            {
                IsTracingActive = false;
                EndTracingPlayer();
            }
        }
    }

    public void StartTracing()
    {
        IsTracingActive = true;
        TracingTimer = DefaultTracingTime;

        Debug.Log("Tracing has started.");
        OnTracingStarted?.Invoke();
    }

    public void TryIncreaseTracingSpeed()
    {
        currentTracingSpeedLevel = Mathf.Clamp(currentTracingSpeedLevel + 1, 0, tracingSpeedLevels.Length - 1);
        tracingSpeedMultiplier = tracingSpeedLevels[currentTracingSpeedLevel];
    }

    public float GetRealRemainingTracingTime()
    {
        return TracingTimer / tracingSpeedMultiplier;
    }

    private void InterruptTracing()
    {
        IsTracingActive = false;
    }

    private void EndTracingPlayer()
    {
        Debug.Log("Player has been traced.");

        if (!ServerConnectionManager.Instance.TryDeleteCurrentServer())
        {
            Debug.LogWarning("Tracing ended when no active server connection!");
        }
        PowerManager.Instance.SetPowerState(false);
        OnPlayerTraced?.Invoke();
        DetectionManager.Instance.ResetDetectionChance();
    }
}
