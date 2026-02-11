using System;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager Instance { get; private set; }

    public event Action OnDetectionOccured;
    public event Action<int> OnDetectionChanceChanged;

    public int CurrentDetectionChance { get; private set; }

    private int currentDetectionLevel;
    public const int DefaultDetectionLevel = 0;

    private readonly int[] detectionLevels = { 2, 5, 10, 25, 40, 70, 98, 100 };
    // private static readonly int[] detectionLevels = { -1 };

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
        SetDetectionChance();
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

            OnDetectionOccured?.Invoke();
        }
        else
        {
            IncreaseDetectionChance();
        }
    }

    public void ResetDetectionChance()
    {
        currentDetectionLevel = DefaultDetectionLevel;
        SetDetectionChance();
    }

    private void IncreaseDetectionChance()
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
        OnDetectionChanceChanged?.Invoke(CurrentDetectionChance);
    }
}
