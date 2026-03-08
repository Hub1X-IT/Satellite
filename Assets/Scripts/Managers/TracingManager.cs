using System;
using UnityEngine;

public class TracingManager : MonoBehaviour
{
    public static TracingManager Instance { get; private set; }

    // public event Action OnPlayerTraced; - event for tracing UI

    private float tracingTimer;
    private bool isTracingActive = false;
    private float tracingSpeedMultiplier = 1f;
    private int currentTracingSpeedLevel;

    private const float DefaultTracingTime = 60f;
    // private const float DefaultTracingTime = 5f;
    private readonly float[] tracingSpeedLevels = { 1f, 1.1f, 1.2f }; // to be adjusted later

    public bool IsTracingActive => isTracingActive;

    public float TracingTimer => tracingTimer;

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
        if (isTracingActive)
        {
            tracingTimer -= Time.deltaTime * tracingSpeedMultiplier;
            // Debug.Log("Tracing timer: " + tracingTimer);
            if (tracingTimer <= 0)
            {
                isTracingActive = false;
                EndTracingPlayer();
            }
        }
    }

    public void StartTracing()
    {
        isTracingActive = true;
        tracingTimer = DefaultTracingTime;

        Debug.Log("Tracing has started.");
    }

    public void TryIncreaseTracingSpeed()
    {
        currentTracingSpeedLevel = Mathf.Clamp(currentTracingSpeedLevel + 1, 0, tracingSpeedLevels.Length - 1);
        tracingSpeedMultiplier = tracingSpeedLevels[currentTracingSpeedLevel];
    }

    public float GetRealRemainingTracingTime()
    {
        return tracingTimer / tracingSpeedMultiplier;
    }

    private void InterruptTracing()
    {
        isTracingActive = false;
    }

    private void EndTracingPlayer()
    {
        Debug.Log("Player has been traced.");

        if (!ServerConnectionManager.Instance.TryDeleteCurrentServer())
        {
            Debug.LogWarning("Tracing ended when no active server connection!");
        }
        PowerManager.Instance.SetPowerState(false);
        // OnPlayerTraced?.Invoke();
        DetectionManager.Instance.ResetDetectionChance();
    }
}
