using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvents/GameEvent", order = 0)]
public class GameEventSO : ScriptableObject
{
    public event Action EventRaised;

    [SerializeField]
    private bool oneTimeTrigger = false;

    [SerializeField]
    private GameEventSO[] previousGameEvents;

    public void TryRaiseEvent()
    {
        if (oneTimeTrigger && GameEventOrderManager.WasGameEventRaised(this))
        {
            return;
        }

        foreach (var previousGameEvent in previousGameEvents)
        {
            if (!GameEventOrderManager.WasGameEventRaised(previousGameEvent))
            {
                return;
            }
        }

        EventRaised?.Invoke();

        GameEventOrderManager.AddGameEvent(this);
    }

    public void ForceRaiseEvent()
    {
        EventRaised?.Invoke();
        GameEventOrderManager.AddGameEvent(this);
    }

    public void ResetGameEvent()
    {
        EventRaised = null;
    }
}

public class GameEventSO<T> : ScriptableObject
{
    // This class should not be referenced in other scripts, instead you should reference the child classes.

    // If the GameEvent is raised without arguments, overridenData will become the data for the EventRaised event.
    // This should mostly be used for constant data.

    public event Action<T> EventRaised;
    
    [SerializeField]
    private T overriddenData;

    public void RaiseEvent() => EventRaised?.Invoke(overriddenData);

    public void RaiseEvent(T data) => EventRaised?.Invoke(data);

    public void ResetGameEvent()
    {
        EventRaised = null;
    }
}
