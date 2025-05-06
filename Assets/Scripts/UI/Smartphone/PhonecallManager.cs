using System;
using System.Collections.Generic;
using System.Xml;
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
    [SerializeField]
    private TMP_Text callTime;

    private int callTimeSeconds = 0;
    private int callTimeMinutes = 0;

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

    [Header ("GameEvents")]
    [SerializeField]
    private GameEventSO callAccepted;
    [SerializeField]
    private GameEventSO callTaken;

    [Header("Contact List")]
    [SerializeField]
    private ContactSO[] contactList;

    private void Start()
    {
        acceptCallButton.onClick.AddListener(AcceptCall);
        endCallButton.onClick.AddListener(EndCall);
        stopCallingButton.onClick.AddListener(StopCalling);

        callPlayer(contactList[0].contactName);    
    }
    private void callPlayer(string contactName)
    {
        callerName = contactName;
        callerName1.text = contactName;
        comingCallUI.SetActive(true);
    }
    public void callNPC(string contactName)
    {
        callingName.text = contactName;
        callingUI.SetActive(true);
        InvokeRepeating("CallingSound", 1f, 2f);
        Invoke("NPCTakeCall", 10f);
    }
    private void NPCTakeCall()
    {
        callTaken.TryRaiseEvent();
        callingUI.SetActive(false);
        ongoingCallUI.SetActive(true);
        callerName2.text = callerName;
        InvokeRepeating("AddTime", 0f, 1f);
    }
    private void AcceptCall()
    {
        callAccepted.TryRaiseEvent();
        comingCallUI.SetActive(false);
        ongoingCallUI.SetActive(true);
        callerName2.text = callerName;
        InvokeRepeating("AddTime", 0f, 1f);
    }
    private void EndCall()
    {
        ongoingCallUI.SetActive(false);
    }
    private void StopCalling()
    {
        callingUI.SetActive(false);
    }
    private void AddTime()
    {
        callTimeSeconds += 1;
        if(callTimeSeconds == 60)
        {
            callTimeMinutes += 1;
            callTimeSeconds = 0;
        }
        if(callTimeSeconds <= 9 && callTimeMinutes == 0)
        {
            callTime.text = "00:0" + callTimeSeconds;
        }
        else if(callTimeSeconds >= 10 && callTimeMinutes == 0)
        {
            callTime.text = "00:" + callTimeSeconds;
        }
        else if(callTimeSeconds <= 9 && callTimeMinutes <= 9)
        {
            callTime.text = "0" + callTimeMinutes + ":0" + callTimeSeconds;
        }
        else if(callTimeSeconds >= 10 && callTimeMinutes >= 10)
        {
            callTime.text = callTimeMinutes + ":" + callTimeSeconds;
        }
    }
}