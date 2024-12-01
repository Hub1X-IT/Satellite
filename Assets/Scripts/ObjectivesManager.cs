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
        // public ChapterSO ChapterSO;
        // public ObjectiveSO ObjectiveSO;
    }

    [SerializeField]
    private ObjectivesUI objectivesUI;

    [SerializeField]
    private string defaultChapter;

    [SerializeField]
    private string defaultObjective;

    [SerializeField]
    private ObjectiveData[] objectivesData;

    private void Awake()
    {
        objectivesUI.SetChapter(defaultChapter);
        objectivesUI.SetObjective(defaultObjective);

        foreach (var objectiveData in objectivesData)
        {
            foreach (var gameEvent in objectiveData.GameEvents)
            {
                gameEvent.EventRaised += () =>
                {
                    if (objectiveData.Chapter != string.Empty)
                    {
                        objectivesUI.SetChapter(objectiveData.Chapter);
                    }
                    if (objectiveData.Objective != string.Empty)
                    {
                        objectivesUI.SetObjective(objectiveData.Objective);
                    }
                };
            }
        }
    }
}