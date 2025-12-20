using System;
using UnityEngine;

public class PhonecallManager : MonoBehaviour
{
    public static PhonecallManager Instance { get; private set; }

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
        public bool CanBeEnded;

        public Action OnIncomingCallAnswered;
        public Action OnCallStopped;
        public Action OnStoppedCalling;
    }

    public event Action<Call> NewCallStarted;
    public event Action CurrentCallEnded;
    public event Action<bool> OnCanEndCall;

    private Call currentCall;

    [SerializeField]
    private float outgoingCallTime;

    [SerializeField]
    private GameEventContactSO callAcceptedGameEvent;
    [SerializeField]
    private GameEventContactSO outgoingCallStartedGameEvent;

    [SerializeField]
    private ContactSO[] contactList;

    private bool isOutgoingCallActive;
    private float outgoingCallTimer;

    public ContactSO[] ContactList => contactList;
    public Call CurrentCall => currentCall;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple Instances of {nameof(PhonecallManager)} detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach (var contactSO in ContactList)
        {
            contactSO.InitializeContactSO();
        }

        isOutgoingCallActive = false;
        outgoingCallTimer = 0;
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

    private void OnDestroy()
    {
        NewCallStarted = null;
        CurrentCallEnded = null;
        OnCanEndCall = null;
        callAcceptedGameEvent.ResetGameEvent();
        outgoingCallStartedGameEvent.ResetGameEvent();
    }

    private Call StartCall(Call call)
    {
        currentCall = call;
        NewCallStarted?.Invoke(call);

        OnCanEndCall?.Invoke(call.CanBeEnded);

        return call;
    }

    private Call StartCall(CallType callType, ContactSO contactSO, bool canBeEnded)
    {
        return StartCall(new Call()
        {
            CallType = callType,
            ContactSO = contactSO,
            CanBeEnded = canBeEnded
        });
    }

    public void AcceptIncomingCall()
    {
        if (currentCall != null && currentCall.CallType == CallType.IncomingCall)
        {
            Call newCall = currentCall;
            newCall.CallType = CallType.OngoingCall;
            StopCurrentCall();
            StartCall(newCall);
            newCall.OnIncomingCallAnswered?.Invoke();
            callAcceptedGameEvent.RaiseEvent(newCall.ContactSO);
            newCall.ContactSO.InvokePhoneAnsweredGameEvents();
        }
        else
        {
            Debug.LogWarning("AcceptCall invoked when no incoming call");
        }
    }

    public bool TryEndOngoingCall()
    {
        if (currentCall != null && currentCall.CallType == CallType.OngoingCall)
        {
            ContactSO contactSO = currentCall.ContactSO;
            StopCurrentCall();
            contactSO.InvokeCallEndedGameEvents();
            return true;
        }
        else
        {
            Debug.LogWarning("EndCall invoked when no ongoing call");
            return false;
        }
    }

    public void StopCalling()
    {
        if (currentCall != null && currentCall.CallType == CallType.OutgoingCall)
        {
            currentCall.OnStoppedCalling?.Invoke();
            StopCurrentCall();
        }
        else
        {
            Debug.LogWarning("StopCalling invoked when no outcoming call");
        }
    }

    private void AnswerOutgoingCall()
    {
        isOutgoingCallActive = false;

        if (currentCall != null && currentCall.CallType == CallType.OutgoingCall)
        {
            if (currentCall.ContactSO.CanPhoneBeAnswered)
            {
                Call newCall = currentCall;
                newCall.CallType = CallType.OngoingCall;
                StopCurrentCall();
                newCall.ContactSO.InvokeOutgoingCallAnsweredGameEvents();
                StartCall(newCall);
            }
        }
        else
        {
            Debug.LogWarning("AnswerOutgoingCall invoked when no outcoming call");
        }
    }

    private void StopCurrentCall()
    {
        currentCall.OnCallStopped?.Invoke();
        CurrentCallEnded?.Invoke();
        currentCall = null;
    }

    public void SetCanEndCall(bool canEnd)
    {
        OnCanEndCall?.Invoke(canEnd);
    }

    public Call StartIncomingCall(ContactSO contactSO, bool canBeEnded)
    {
        return StartCall(CallType.IncomingCall, contactSO, canBeEnded);
    }

    public Call StartOutcomingCall(ContactSO contactSO)
    {
        if (currentCall == null)
        {
            Call newCall = StartCall(CallType.OutgoingCall, contactSO, true);
            outgoingCallStartedGameEvent.RaiseEvent(contactSO);
            contactSO.InvokeOutgoingCallGameEvents();
            isOutgoingCallActive = true;
            outgoingCallTimer = outgoingCallTime;

            return newCall;
        }
        else
        {
            Debug.Log("Can't start outcoming call - a call is already started");
            return null;
        }
    }
}