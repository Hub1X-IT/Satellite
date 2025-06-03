using UnityEngine;

[CreateAssetMenu(fileName = "ContactSO", menuName = "ContactSO")]
public class ContactSO : ScriptableObject
{
    [SerializeField]
    private string contactName;
    
    [Tooltip("Events triggered when an incoming call is answered")]
    [SerializeField]
    private GameEventSO[] phoneAnsweredGameEvents;

    [Tooltip("Events triggered when an outgoing call is started by the player")]
    [SerializeField]
    private GameEventSO[] outgoingCallGameEvents;

    [Tooltip("Events triggered when an outgoing call started by the player is answered")]
    [SerializeField]
    private GameEventSO[] outgoingCallAnsweredGameEvents;

    [Tooltip("Events triggered when a call is ended")]
    [SerializeField]
    private GameEventSO[] callEndedGameEvents;

    // Probably temporary
    [Tooltip("Game events that trigger the ability to end the call; when left empty, the call can be ended from the beginning")]
    public GameEventSO[] CanEndCallGameEvents;

    public string ContactName => contactName;

    public void InvokePhoneAnsweredGameEvents()
    {
        foreach (var gameEvent in phoneAnsweredGameEvents)
        {
            gameEvent.TryRaiseEvent();
        }
    }

    public void InvokeOutgoingCallGameEvents()
    {
        foreach (var gameEvent in outgoingCallGameEvents)
        {
            gameEvent.TryRaiseEvent();
        }
    }

    public void InvokeOutgoingCallAnsweredGameEvents()
    {
        foreach (var gameEvent in outgoingCallAnsweredGameEvents)
        {
            gameEvent.TryRaiseEvent();
        }
    }

    public void InvokeCallEndedGameEvents()
    {
        foreach (var gameEvent in callEndedGameEvents)
        {
            gameEvent.TryRaiseEvent();
        }
    }
}
