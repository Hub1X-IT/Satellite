using System;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    [Serializable]
    private class ObjectiveData
    {
        public GameEventSO[] GameEvents;
        public string Chapter;
        public string Objective;
    }

    [SerializeField]
    private ObjectivesUI[] objectivesUIArray;

    [SerializeField]
    private string defaultChapter;

    [SerializeField]
    private string defaultObjective;

    [SerializeField]
    private ObjectiveData[] objectivesData;

    private void Awake()
    {
        SetChapterAndObjective(defaultChapter, defaultObjective);

        foreach (var objectiveData in objectivesData)
        {
            foreach (var gameEvent in objectiveData.GameEvents)
            {
                gameEvent.EventRaised += () =>
                {
                    SetChapterAndObjective(objectiveData.Chapter, objectiveData.Objective);
                };
            }
        }
    }

    private void SetChapterAndObjective(string chapter, string objective)
    {
        Debug.Log(chapter);
        Debug.Log(objective);
        foreach (var objectivesUI in objectivesUIArray)
        {
            if (chapter.Length > 0)
            {
                objectivesUI.SetChapter(chapter);
            }
            if (objective.Length > 0)
            {
                objectivesUI.SetObjective(objective);
            }
        }
    }
}