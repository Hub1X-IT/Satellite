using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventOrderManager
{
    [Serializable]
    public struct InitializationData
    {
        [Tooltip("Game events to preserve from the previous scene when loading this scene.")]
        public GameEventSO[] gameEventsToPreserve;
    }

    private static HashSet<GameEventSO> raisedGameEvents;

    public static void OnAwake(in InitializationData data)
    {
        if (raisedGameEvents == null)
        {
            raisedGameEvents = new();
            return;
        }

        List<GameEventSO> gameEventsToAdd = new();
        foreach (var gameEventToPreserve in data.gameEventsToPreserve)
        {
            if (raisedGameEvents.Contains(gameEventToPreserve))
            {
                gameEventsToAdd.Add(gameEventToPreserve);
            }
        }

        raisedGameEvents = new();

        foreach (var gameEventToAdd in gameEventsToAdd)
        {
            raisedGameEvents.Add(gameEventToAdd);
        }
    }

    public static void AddGameEvent(GameEventSO gameEvent)
    {
        raisedGameEvents.Add(gameEvent);
    }

    public static bool WasGameEventRaised(GameEventSO gameEvent)
    {
        return raisedGameEvents.Contains(gameEvent);
    }
}