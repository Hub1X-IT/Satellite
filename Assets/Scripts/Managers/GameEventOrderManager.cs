using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventOrderManager : MonoBehaviour
{
    public static GameEventOrderManager Instance { get; private set; }

    [SerializeField]
    private GameEventSO[] gameEventsToPreserve;

    private HashSet<GameEventSO> raisedGameEvents;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(GameEventOrderManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (raisedGameEvents == null)
        {
            raisedGameEvents = new();
            return;
        }

        List<GameEventSO> gameEventsToAdd = new();
        foreach (var gameEventToPreserve in gameEventsToPreserve)
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

    public void AddGameEvent(GameEventSO gameEvent)
    {
        raisedGameEvents.Add(gameEvent);
    }

    public bool WasGameEventRaised(GameEventSO gameEvent)
    {
        return raisedGameEvents.Contains(gameEvent);
    }
}