using System;
using UnityEngine;

public class PhonecallManager : MonoBehaviour
{
    public enum CallType
    {
        IncomingCall,
        OutgoingCall,
        OngoingCall
    }

    public class Call
    {
        public CallType CallType;
        public ContactSO ContactSO;
    }

    public static Action<Call> NewCallStarted;
    public static Action CurrentCallEnded;

    private static Call currentCall;

    [SerializeField]
    private float outgoingCallTime;

    private static float outgoingCallTimeStatic;

    [SerializeField]
    private GameEventContactSO callAcceptedGameEvent;
    [SerializeField]
    private GameEventContactSO outgoingCallStartedGameEvent;

    private static GameEventContactSO callAcceptedGameEventStatic;
    private static GameEventContactSO outgoingCallStartedGameEventStatic;

    [SerializeField]
    private ContactSO[] contactList;

    private static bool isOutgoingCallActive;
    private static float outgoingCallTimer;

    public static ContactSO[] ContactList { get; private set; }

    private void Awake()
    {
        outgoingCallTimeStatic = outgoingCallTime;
        callAcceptedGameEventStatic = callAcceptedGameEvent;
        outgoingCallStartedGameEventStatic = outgoingCallStartedGameEvent;
        ContactList = contactList;

        isOutgoingCallActive = false;
        outgoingCallTimer = 0;

        TempStartCall();
    }

    private void Update()
    {
        // Calling sound and outgoing call timers
        if (isOutgoingCallActive)
        {
            if (outgoingCallTimer <= 0)
            {
                AnswerOutgoingCall();
            }
            else
            {
                outgoingCallTimer -= Time.deltaTime;
            }
        }
    }

    public static void TempStartCall()
    {
        StartCall(new Call()
        {
            CallType = CallType.IncomingCall,
            ContactSO = ContactList[0]
        });
    }

    public static void AcceptCall()
    {
        if (currentCall != null && currentCall.CallType == CallType.IncomingCall)
        {
            Call newCall = currentCall;
            newCall.CallType = CallType.OngoingCall;
            StopCurrentCall();
            StartCall(newCall);
            callAcceptedGameEventStatic.RaiseEvent(newCall.ContactSO);
            newCall.ContactSO.InvokePhoneAnsweredGameEvents();
        }
        else
        {
            Debug.LogWarning("AcceptCall invoked when no incoming call");
        }
    }

    public static void EndCall()
    {
        if (currentCall != null && currentCall.CallType == CallType.OngoingCall)
        {
            ContactSO contactSO = currentCall.ContactSO;
            StopCurrentCall();
            contactSO.InvokeCallEndedGameEvents();
        }
        else
        {
            Debug.LogWarning("EndCall invoked when no ongoing call");
        }
    }

    public static void StopCalling()
    {
        if (currentCall != null && currentCall.CallType == CallType.OutgoingCall)
        {
            StopCurrentCall();
        }
        else
        {
            Debug.LogWarning("StopCalling invoked when no outcoming call");
        }
    }

    private static void AnswerOutgoingCall()
    {
        isOutgoingCallActive = false;

        if (currentCall != null && currentCall.CallType == CallType.OutgoingCall)
        {
            Call newCall = currentCall;
            newCall.CallType = CallType.OngoingCall;
            StopCurrentCall();
            newCall.ContactSO.InvokeOutgoingCallAnsweredGameEvents();
            StartCall(newCall);
        }
        else
        {
            Debug.LogWarning("AnswerOutgoingCall invoked when no outcoming call");
        }
    }

    private static void StopCurrentCall()
    {
        CurrentCallEnded?.Invoke();
        currentCall = null;
    }

    private static void StartCall(Call call)
    {
        currentCall = call;
        NewCallStarted?.Invoke(call);
    }

    private static void StartCall(CallType callType, ContactSO contactSO)
    {
        StartCall(new Call()
        {
            CallType = callType,
            ContactSO = contactSO,
        });
    }

    public static void StartOutcomingCall(ContactSO contactSO)
    {
        if (currentCall == null)
        {
            StartCall(CallType.OutgoingCall, contactSO);
            outgoingCallStartedGameEventStatic.RaiseEvent(contactSO);
            contactSO.InvokeOutgoingCallGameEvents();
            isOutgoingCallActive = true;
            outgoingCallTimer = outgoingCallTimeStatic;
        }
        else
        {
            Debug.Log("Can't start outcoming call - a call is already started");
        }
    }
}