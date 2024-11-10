using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvents/GameEvent", order = 0)]
public class NewGameEventSO : ScriptableObject
{
    public event Action EventRaised;

    public void RaiseEvent() => EventRaised?.Invoke();
}

public class NewGameEventSO<T> : ScriptableObject
{
    public event Action<T> EventRaised;

    public void RaiseEvent(T data) => EventRaised?.Invoke(data);
}

[CreateAssetMenu(menuName = "GameEvents/GameEvent<bool>", order = 1)]
public class NewGameEventBoolSO : NewGameEventSO<bool> { }

[CreateAssetMenu(menuName = "GameEvents/GameEvent<Computer>", order = 1)]
public class NewGameEventComputerSO : NewGameEventSO<Computer> { }
