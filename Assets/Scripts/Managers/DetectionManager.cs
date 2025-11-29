using System;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager Instance { get; private set; }

    public event Action DetectionOccured;
    public event Action DetectionRemoved;

    public event Action<bool> ServerPowerEnabled;

    [SerializeField]
    private GameEventSO onDetectionOccuredGameEvent;
    [SerializeField]
    private GameEventSO onDetectionRemovedGameEvent;

    public int CurrentDetectionChance { get; private set; }

    private int currentDetectionLevel;
    private const int DefaultDetectionLevel = 0;

    private readonly int[] detectionLevels = { 2, 5, 10, 25, 40, 70, 98, 100 };
    // Debug detection level:
    // private static readonly int[] detectionLevels = { -1 };

    public bool WasDetected { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(DetectionManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentDetectionLevel = DefaultDetectionLevel;
        WasDetected = false;
        SetDetectionChance();
    }

    private void OnDestroy()
    {
        onDetectionOccuredGameEvent.ResetGameEvent();
        onDetectionRemovedGameEvent.ResetGameEvent();
    }

    public void CheckDetection()
    {
        int randomDetectionChance = UnityEngine.Random.Range(0, 100);

        if (randomDetectionChance < CurrentDetectionChance)
        {
            WasDetected = true;
            DetectionOccured?.Invoke();
            if (onDetectionOccuredGameEvent != null)
            {
                onDetectionOccuredGameEvent.TryRaiseEvent();
            }
        }
        else
        {
            IncreaseDetectionLevel();
        }
        // Debug.Log($"Current detection chance: {detectionLevels[currentDetectionLevel]}");
        // Debug.Log($"{(WasDetected ? "" : "Not ")}Detected");
    }

    public void SetServerPowerEnabled(bool enabled)
    {
        ServerPowerEnabled?.Invoke(enabled);
        if (enabled && WasDetected)
        {
            ResetDetection();
        }
    }

    private void ResetDetection()
    {
        WasDetected = false;
        currentDetectionLevel = DefaultDetectionLevel;
        SetDetectionChance();
        DetectionRemoved?.Invoke();
        if (onDetectionRemovedGameEvent != null)
        {
            onDetectionRemovedGameEvent.TryRaiseEvent();
        }
    }

    private void IncreaseDetectionLevel()
    {
        if (currentDetectionLevel < detectionLevels.Length - 1)
        {
            currentDetectionLevel++;
            SetDetectionChance();
        }
    }

    private void SetDetectionChance()
    {
        CurrentDetectionChance = detectionLevels[currentDetectionLevel];
    }
}
