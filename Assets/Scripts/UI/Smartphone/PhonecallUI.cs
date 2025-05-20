using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhonecallUI : MonoBehaviour
{
    [Header("Incoming Call")]
    [SerializeField]
    private GameObject incomingCallUI;
    [SerializeField]
    private Button acceptCallButton;
    [SerializeField]
    private TMP_Text incomingCallCallerNameTextField;

    [Header("Ongoing Call")]
    [SerializeField]
    private GameObject ongoingCallUI;
    [SerializeField]
    private Button endCallButton;
    [SerializeField]
    private TMP_Text ongoingCallCallerNameTextField;
    [SerializeField]
    private TMP_Text callTimeTextField;

    [Header("Outgoing call")]
    [SerializeField]
    private GameObject outgoingCallUI;
    [SerializeField]
    private Button stopCallingButton;
    [SerializeField]
    private TMP_Text callingNameTextField;
    [SerializeField]
    private AudioSource callingSound;

    private bool isOutgoingCallActive;
    private float callingSoundTimer;
    private const float CallingSoundInterval = 2f;

    private bool isCallTimerActive;
    private float callTimeTimer;
    private int callTime;

    private void Start()
    {
        acceptCallButton.onClick.AddListener(PhonecallManager.AcceptCall);
        endCallButton.onClick.AddListener(PhonecallManager.EndCall);
        stopCallingButton.onClick.AddListener(PhonecallManager.StopCalling);

        PhonecallManager.NewCallStarted += OnNewCallStarted;
        PhonecallManager.CurrentCallEnded += StopCall;

        PhonecallManager.TempStartCall();
    }

    private void Update()
    {
        // Calling sound timer
        if (isOutgoingCallActive)
        {
            if (callingSoundTimer <= 0)
            {
                callingSound.Play();
                callingSoundTimer = CallingSoundInterval;
            }
            else
            {
                callingSoundTimer -= Time.deltaTime;
            }
        }

        // CallTimer
        if (isCallTimerActive)
        {
            if (callTimeTimer <= 0)
            {
                callTime += 1;
                SetCallTimerText();
                callTimeTimer = 1f;
            }
            else
            {
                callTimeTimer -= Time.deltaTime;
            }
        }
    }

    private void OnNewCallStarted(PhonecallManager.Call newCall)
    {
        string contactName = newCall.ContactSO.ContactName;
        switch (newCall.CallType)
        {
            case PhonecallManager.CallType.IncomingCall:
                StartIncomingCall(contactName);
                break;
            case PhonecallManager.CallType.OutgoingCall:
                StartOutgoingCall(contactName);
                break;
            case PhonecallManager.CallType.OngoingCall:
                StartOngoingCall(contactName);
                break;
        }
    }

    private void StartIncomingCall(string contactName)
    {
        incomingCallCallerNameTextField.text = contactName;
        incomingCallUI.SetActive(true);
        Debug.Log("inc");
    }

    private void StartOutgoingCall(string contactName)
    {
        callingNameTextField.text = contactName;
        outgoingCallUI.SetActive(true);
        isOutgoingCallActive = true;
        callingSoundTimer = 0f;
    }

    private void StartOngoingCall(string contactName)
    {
        ongoingCallCallerNameTextField.text = contactName;
        ongoingCallUI.SetActive(true);
        isCallTimerActive = true;
        callTimeTimer = 1f;
        callTime = 0;
        SetCallTimerText();
    }

    private void StopCall()
    {
        incomingCallUI.SetActive(false);
        outgoingCallUI.SetActive(false);
        ongoingCallUI.SetActive(false);
        isOutgoingCallActive = false;
        isCallTimerActive = false;
        callingSound.Stop();
    }

    private void SetCallTimerText()
    {
        int callTimeMinutes = callTime / 60;
        int callTimeSeconds = callTime % 60;

        string callTimeTextMinutes = callTimeMinutes <= 9 ? "0" + callTimeMinutes : callTimeMinutes.ToString();
        string callTimeTextSeconds = callTimeSeconds <= 9 ? "0" + callTimeSeconds : callTimeSeconds.ToString();

        callTimeTextField.text = callTimeTextMinutes + ":" + callTimeTextSeconds;
    }
}