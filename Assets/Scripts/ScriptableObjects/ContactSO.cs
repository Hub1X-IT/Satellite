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

    [Tooltip("Default value for the receiver's ability to answer the phone when the player is calling")]
    [SerializeField]
    private bool phoneCanBeAnsweredDefault;

    [Tooltip("Game events that enable the receiver to answer the phone when the player is calling")]
    [SerializeField]
    private GameEventSO[] phoneCanBeAnsweredGameEvents;

    [Tooltip("Game events that disable the receiver to answer the phone when the player is calling")]
    [SerializeField]
    private GameEventSO[] phoneCantBeAnsweredGameEvents;

    public bool CanPhoneBeAnswered { get; set; }

    public string ContactName => contactName;

    public void InitializeContactSO()
    {
        CanPhoneBeAnswered = phoneCanBeAnsweredDefault;

        foreach (var gameEvent in phoneCanBeAnsweredGameEvents)
        {
            gameEvent.EventRaised += () => CanPhoneBeAnswered = true;
        }
        foreach (var gameEvent in phoneCantBeAnsweredGameEvents)
        {
            gameEvent.EventRaised += () => CanPhoneBeAnswered = false;
        }
    }

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
