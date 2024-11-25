using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ObjectivesUI : MonoBehaviour
{
    [Serializable]
    private class ObjectiveData
    {
        public GameEventSO[] GameEvents;
        public string Chapter;
        public string Objective;
        // public ChapterSO ChapterSO;
        // public ObjectiveSO ObjectiveSO;
        // public UnityEvent OnGameEventRaised;
    }

    [SerializeField]
    private TMP_Text chapterTextField;

    [SerializeField]
    private TMP_Text objectiveTextField;

    [SerializeField]
    private string defaultChapter;

    [SerializeField]
    private string defaultObjective;

    [SerializeField]
    private ObjectiveData[] objectivesData;

    private void Awake()
    {
        SetChapter(defaultChapter);
        SetObjective(defaultObjective);

        foreach (var objectiveData in objectivesData)
        {
            foreach (var gameEvent in objectiveData.GameEvents)
            {
                gameEvent.EventRaised += () =>
                {
                    if (objectiveData.Chapter != string.Empty)
                    {
                        SetChapter(objectiveData.Chapter);
                    }
                    if (objectiveData.Objective != string.Empty)
                    {
                        SetObjective(objectiveData.Objective);
                    }
                };
            }
        }
    }

    public void SetChapter(string chapter)
    {
        chapterTextField.text = chapter;
    }

    public void SetChapter(ChapterSO chapter)
    {
        chapterTextField.text = chapter.Title;
    }

    public void SetObjective(string objective)
    {
        objectiveTextField.text = objective;
    }

    public void SetObjective(ObjectiveSO objective)
    {
        objectiveTextField.text = objective.Objective;
    }
}