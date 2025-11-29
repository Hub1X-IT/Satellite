using System;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    public static ObjectivesManager Instance { get; private set; }

    public event Action<string> OnObjectiveChanged;
    public event Action<string> OnChapterChanged;

    [SerializeField]
    private string defaultChapter;

    [SerializeField]
    private string defaultObjective;

    private string currentChapter;
    private string currentObjective;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple Instances of {nameof(ObjectivesManager)} detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetChapter(defaultChapter);
        SetObjective(defaultObjective);
    }

    public void SetChapter(string chapter)
    {
        currentChapter = chapter;
        OnChapterChanged?.Invoke(chapter);
    }

    public void SetObjective(string objective)
    {
        currentObjective = objective;
        OnObjectiveChanged?.Invoke(objective);
    }
}