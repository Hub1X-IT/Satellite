using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvents/GameEvent", order = 0)]
public class GameEventSO : ScriptableObject
{
    public event Action EventRaised;

    public void RaiseEvent() => EventRaised?.Invoke();
}

public class GameEventSO<T> : ScriptableObject
{
    // This class should not be referenced in other scripts, instead you should reference the child classes.

    // If the GameEvent is raised without arguments, overridenData will become the data for the EventRaised event.
    // This should mostly be used for constant data and not for references to objects within the scene.

    public event Action<T> EventRaised;
    
    [SerializeField]
    private T overriddenData;

    public void RaiseEvent() => EventRaised?.Invoke(overriddenData);

    public void RaiseEvent(T data) => EventRaised?.Invoke(data);
}
