using UnityEngine;

[CreateAssetMenu(fileName = "ContactSO", menuName = "Scriptable Objects/ContactSO")]
public class ContactSO : ScriptableObject
{
    [SerializeField]
    private string contactName;

    [SerializeField]
    private GameEventSO[] phoneAnsweredGameEvents;

    [SerializeField]
    private GameEventSO[] outgoingCallGameEvents;

    [SerializeField]
    private GameEventSO[] outgoingCallAnsweredGameEvents;

    [SerializeField]
    private GameEventSO[] callEndedGameEvents;

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
