using System;
using UnityEngine;

public static class PhonecallManager
{
    [Serializable]
    public struct InitializationData
    {
        public GameEventContactSO CallAcceptedGameEvent;
        public GameEventContactSO OutgoingCallStartedGameEvent;
        public ContactSO[] ContactList;
        public float OutgoingCallTime;
    }

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

    private static float outgoingCallTime;
    public static float OutgoingCallTime => outgoingCallTime;

    private static GameEventContactSO callAcceptedGameEvent;
    private static GameEventContactSO outgoingCallStartedGameEvent;

    private static ContactSO[] contactList;

    public static ContactSO[] ContactList => contactList;

    public static void OnAwake(InitializationData initializationData)
    {
        callAcceptedGameEvent = initializationData.CallAcceptedGameEvent;
        outgoingCallStartedGameEvent = initializationData.OutgoingCallStartedGameEvent;
        contactList = initializationData.ContactList;
        outgoingCallTime = initializationData.OutgoingCallTime;
    }

    public static void TempStartCall()
    {
        StartCall(new Call()
        {
            CallType = CallType.IncomingCall,
            ContactSO = contactList[0]
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
            callAcceptedGameEvent?.RaiseEvent(newCall.ContactSO);
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

    public static void AnswerOutgoingCall()
    {
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
            outgoingCallStartedGameEvent?.RaiseEvent(contactSO);
            contactSO.InvokeOutgoingCallGameEvents();
        }
        else
        {
            Debug.Log("Can't start outcoming call - a call is already started");
        }
    }
}