using System;
using UnityEngine;

public static class DetectionManager
{
    [Serializable]
    public struct InitializationData
    {
        public int DefaultDetectionChance;
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

    private static int[] levels = { 2, 5, 10, 25, 40, 70, 98, 100};

    public static bool WasDetected { get; private set; }

    public static void InitializeDetectionManager(InitializationData data)
    {
        currentDetectionLevel = DefaultDetectionLevel;
        CurrentDetectionChance = data.DefaultDetectionChance;
        onDetectionOccuredGameEvent = data.onDetectionOccuredGameEvent;
        onDetectionRemovedGameEvent = data.onDetectionRemovedGameEvent;
        WasDetected = false;
    }

    public static void OnSceneExit()
    {
        DetectionOccured = null;
        DetectionRemoved = null;
        ServerPowerEnabled = null;
    }

    public static void CheckDetection()
    {
        int randomDetectionChance = UnityEngine.Random.Range(100, 0);
        
        // randomDetectionChance = UnityEngine.Random.Range(0, 100);
        // if (randomDetectionChance < levels[currentDetectionLevel])

        if (randomDetectionChance < levels[currentDetectionLevel])
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
        Debug.Log($"Current detection chance: {levels[currentDetectionLevel]}");
        Debug.Log($"{(WasDetected ? "" : "Not ")}Detected");
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
        CurrentDetectionChance = levels[DefaultDetectionLevel];
        DetectionRemoved?.Invoke();
        if (onDetectionRemovedGameEvent != null)
        {
            onDetectionRemovedGameEvent.TryRaiseEvent();
        }
    }

    const int maxDetectionLevel = 7;

    private static void IncreaseDetectionLevel()
    {
        if (currentDetectionLevel < maxDetectionLevel)
        {
            currentDetectionLevel++;
        }

        // OBSOLETE
        // Works only if detection chance is less than or equal to 8!
        // CurrentDetectionChance = (int)(-1.7 * currentDetectionLevel * currentDetectionLevel + 1.5 * currentDetectionLevel + 98);
        // END OBSOLETE
    }
}
