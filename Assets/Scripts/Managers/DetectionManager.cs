using System;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager Instance { get; private set; }

    public event Action<int> OnDetectionChanceChanged;
    public event Action OnDetectionLevelIncreased;

    public int CurrentDetectionChance { get; private set; }

    private int currentDetectionLevel;
    private const int DefaultDetectionLevel = 0;

    private readonly int[] detectionLevels = { 0, 2, 5, 10, 25, 40, 70, 98, 100 };
    // private readonly int[] detectionLevels = { -1 };

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
        UpdateDetectionChance();
    }

    public void CheckDetection()
    {
        int randomDetectionChance = UnityEngine.Random.Range(0, 100);

        if (randomDetectionChance < CurrentDetectionChance)
        {
            if (!TracingManager.Instance.IsTracingActive)
            {
                TracingManager.Instance.StartTracing();
            }
            else
            {
                TracingManager.Instance.TryIncreaseTracingSpeed();
            }
        }
        else
        {
            IncreaseDetectionChance();
        }
    }

    public bool TrySetDetectionChance(int detectionChance)
    {
        int valueIndex = Array.IndexOf(detectionLevels, detectionChance);
        if (valueIndex >= 0)
        {
            currentDetectionLevel = valueIndex;
            UpdateDetectionChance();
            return true;
        }
        else
        {
            // currentDetectionLevel = DefaultDetectionLevel;
        }
        return false;
    }

    public void ResetDetectionChance()
    {
        currentDetectionLevel = DefaultDetectionLevel;
        UpdateDetectionChance();
    }

    private void IncreaseDetectionChance()
    {
        if (currentDetectionLevel < detectionLevels.Length - 1)
        {
            currentDetectionLevel++;
            UpdateDetectionChance();
            OnDetectionLevelIncreased?.Invoke();
        }
    }

    private void UpdateDetectionChance()
    {
        CurrentDetectionChance = detectionLevels[currentDetectionLevel];
        OnDetectionChanceChanged?.Invoke(CurrentDetectionChance);
    }
}
