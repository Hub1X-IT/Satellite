using System;
using UnityEngine;

public static class DetectionManager
{
    [Serializable]
    public struct InitializationData
    {
        public int DefaultDetectionChance;
    }

    public static event Action DetectionOccured;
    public static event Action DetectionRemoved;

    public static event Action<bool> ServerPowerEnabled;

    public static int CurrentDetectionChance { get; private set; }
    private static int defaultDetectionChance;

    private static int currentDetectionLevel;
    private const int DefaultDetectionLevel = 1;

    public static bool WasDetected { get; private set; }

    public static void InitializeDetectionManager(InitializationData data)
    {
        currentDetectionLevel = DefaultDetectionLevel;
        defaultDetectionChance = CurrentDetectionChance = data.DefaultDetectionChance;
    }

    public static void OnSceneExit()
    {
        DetectionOccured = null;
        DetectionRemoved = null;
        ServerPowerEnabled = null;
    }

    public static void CheckDetection()
    {
        int randomDetectionChance = UnityEngine.Random.Range(1, CurrentDetectionChance + 1);
        if (randomDetectionChance == CurrentDetectionChance)
        {
            WasDetected = true;
            DetectionOccured?.Invoke();
            Debug.Log("Detected!");
        }
        else
        {
            IncreaseDetectionChance();
            Debug.Log($"Current detection chance: {CurrentDetectionChance}");
        }
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
        DetectionRemoved?.Invoke();
        WasDetected = false;
        currentDetectionLevel = DefaultDetectionLevel;
        CurrentDetectionChance = defaultDetectionChance;
    }

    private static void IncreaseDetectionChance()
    {
        int maxDetectionLevel = 8;
        if (currentDetectionLevel < maxDetectionLevel)
        {
            currentDetectionLevel++;
        }
        // Works only if detection chance is less or equal to 8!
        CurrentDetectionChance = (int)(-1.7 * currentDetectionLevel * currentDetectionLevel + 1.5 * currentDetectionLevel + 98);
    }
}
