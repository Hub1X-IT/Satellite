using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEventSO[] gameEvents;

    public UnityEvent OnGameEventRaised;

    private void Awake()
    {
        foreach (var gameEvent in gameEvents)
        {
            gameEvent.EventRaised += InvokeUnityEvent;
        }
    }

    /*
    private void OnEnable()
    {
        foreach (var gameEvent in gameEvents)
        {
            gameEvent.EventRaised += InvokeUnityEvent;
        }
    }

    private void OnDisable()
    {
        foreach (var gameEvent in gameEvents)
        {
            gameEvent.EventRaised -= InvokeUnityEvent;
        }
    }
    */
    /*
    private void OnDestroy()
    {
        foreach (var gameEvent in gameEvents)
        {
            gameEvent.EventRaised -= InvokeUnityEvent;
        }
    }
    */

    private void InvokeUnityEvent()
    {
        OnGameEventRaised?.Invoke();
    }
}
