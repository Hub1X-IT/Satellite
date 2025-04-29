using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhonecallManager : MonoBehaviour
{
    [Header ("ComingCallUI")]
    [SerializeField]
    private GameObject comingCallUI;
    [SerializeField]
    private Button acceptCallButton;
    [SerializeField]
    private TMP_Text callerName1;


    [Header ("OngoingCallUI")]
    [SerializeField]
    private GameObject ongoingCallUI;
    [SerializeField]
    private Button endCallButton;
    [SerializeField]
    private TMP_Text callerName2;

    [Header ("CallingUI")]
    [SerializeField]
    private GameObject callingUI;
    [SerializeField]
    private Button stopCallingButton;
    [SerializeField]
    private TMP_Text callingName;
    [SerializeField]
    private AudioSource callingSound;

    private string callerName;

    public event Action callAccepted;
    public event Action callSent;

    private void Start()
    {
        acceptCallButton.onClick.AddListener(AcceptCall);
        endCallButton.onClick.AddListener(EndCall);
        stopCallingButton.onClick.AddListener(StopCalling);

        callPlayer("Sp1k3");
    }
    private void callPlayer(string callerNameTemp)
    {
        callerName = callerNameTemp;
        callerName1.text = callerNameTemp;
        comingCallUI.SetActive(true);
    }
    private void callNPC(string callerNameTemp)
    {
        callerName = callerNameTemp;
        callingName.text = callerNameTemp;
        callingUI.SetActive(true);
        InvokeRepeating("CallingSound", 1f, 2f);
        Invoke("NPCTakeCall", 10f);
    }
    private void NPCTakeCall()
    {
        callingUI.SetActive(false);
        ongoingCallUI.SetActive(true);
        callerName2.text = callerName;
        callSent.Invoke();
    }
    private void AcceptCall()
    {
        comingCallUI.SetActive(false);
        ongoingCallUI.SetActive(true);
        callerName2.text = callerName;
        callAccepted.Invoke();
    }
    private void EndCall()
    {
        ongoingCallUI.SetActive(false);
    }
    private void StopCalling()
    {
        callingUI.SetActive(false);
    }
}
