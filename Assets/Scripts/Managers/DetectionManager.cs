using System;
using UnityEngine;

public static class DetectionManager
{
    [Serializable]
    public struct InitializationData
    {
        // Probably a temporary solution; only for demo level / level 1
        public GameEventSO onDetectionOccuredGameEvent;
        public GameEventSO onDetectionRemovedGameEvent;
    }

    public static event Action DetectionOccured;
    public static event Action DetectionRemoved;

    public static event Action<bool> ServerPowerEnabled;

    private static GameEventSO onDetectionOccuredGameEvent;
    private static GameEventSO onDetectionRemovedGameEvent;

    public static int CurrentDetectionChance { get; private set; }

    private static int currentDetectionLevel;
    private const int DefaultDetectionLevel = 0;

    private static readonly int[] detectionLevels = { 2, 5, 10, 25, 40, 70, 98, 100 };
    // Debug detection level:
    // private static readonly int[] detectionLevels = { -1 };

    public static bool WasDetected { get; private set; }

    public static void InitializeDetectionManager(InitializationData data)
    {
        currentDetectionLevel = DefaultDetectionLevel;
        onDetectionOccuredGameEvent = data.onDetectionOccuredGameEvent;
        onDetectionRemovedGameEvent = data.onDetectionRemovedGameEvent;
        WasDetected = false;
        SetDetectionChance();
    }

    public static void OnSceneExit()
    {
        DetectionOccured = null;
        DetectionRemoved = null;
        ServerPowerEnabled = null;
        onDetectionOccuredGameEvent.ResetGameEvent();
        onDetectionRemovedGameEvent.ResetGameEvent();
    }

    public static void CheckDetection()
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

    public static void SetServerPowerEnabled(bool enabled)
    {
        ServerPowerEnabled?.Invoke(enabled);
        if (enabled && WasDetected)
        {
            ResetDetection();
        }
    }

    private static void ResetDetection()
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

    private static void IncreaseDetectionLevel()
    {
        if (currentDetectionLevel < detectionLevels.Length - 1)
        {
            currentDetectionLevel++;
            SetDetectionChance();
        }
    }

    private static void SetDetectionChance()
    {
        CurrentDetectionChance = detectionLevels[currentDetectionLevel];
    }
}
