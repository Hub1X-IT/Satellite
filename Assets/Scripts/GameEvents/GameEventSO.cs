using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvents/GameEvent", order = 0)]
public class GameEventSO : ScriptableObject
{
    public event Action EventRaised;

    public void RaiseEvent()
    {
        EventRaised?.Invoke();
    }

    public void ResetGameEvent()
    {
        EventRaised = null;
    }
}

public class GameEventSO<T> : ScriptableObject
{
    // This class should not be referenced in other scripts, instead you should reference the child classes.

    public event Action<T> EventRaised;

    public void RaiseEvent(T data) => EventRaised?.Invoke(data);

    public void ResetGameEvent()
    {
        EventRaised = null;
    }
}
