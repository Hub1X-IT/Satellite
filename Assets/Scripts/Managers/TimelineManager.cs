using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [Serializable]
    private class TimelineTrigger
    {
        public GameEventSO[] TriggerGameEvents;
        public PlayableDirector TimlelinePlayableDirector;
    }

    [SerializeField]
    private TimelineTrigger[] timelineTriggers;

    private void Awake()
    {
        foreach (var timelineTrigger in timelineTriggers)
        {
            foreach (var gameEvent in timelineTrigger.TriggerGameEvents)
            {
                gameEvent.EventRaised += () => StartTimeline(timelineTrigger.TimlelinePlayableDirector);
            }
        }
    }

    private void StartTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Play();
    }
}
