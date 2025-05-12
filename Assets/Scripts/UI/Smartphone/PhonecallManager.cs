using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhonecallManager : MonoBehaviour
{
    [Header("ComingCallUI")]
    [SerializeField]
    private GameObject comingCallUI;
    [SerializeField]
    private Button acceptCallButton;
    [SerializeField]
    private TMP_Text callerName1;


    [Header("OngoingCallUI")]
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

    [Header("CallingUI")]
    [SerializeField]
    private GameObject callingUI;
    [SerializeField]
    private Button stopCallingButton;
    [SerializeField]
    private TMP_Text callingName;
    [SerializeField]
    private AudioSource callingSound;

    private string callerName;

    [Header("GameEvents")]
    [SerializeField]
    private GameEventSO callAccepted;
    [SerializeField]
    private GameEventSO callTaken;

    [Header("Contact List")]
    [SerializeField]
    private ContactSO[] contactList;

    [Header("Objectives")]
    [SerializeField]
    private GameEventSO objectiveAnswerPhone;
    [SerializeField]
    private GameEventSO objectiveCallSomeone;
    [SerializeField]
    private GameEventSO[] nextObjectives;
    private int objective = 0;

    private void Start()
    {
        acceptCallButton.onClick.AddListener(AcceptCall);
        endCallButton.onClick.AddListener(EndCall);
        stopCallingButton.onClick.AddListener(StopCalling);

        CallPlayer(contactList[0].ContactName);
    }

    private void CallPlayer(string contactName)
    {
        callerName = contactName;
        callerName1.text = contactName;
        comingCallUI.SetActive(true);
    }

    public void CallNPC(string contactName)
    {
        callingName.text = contactName;
        callingUI.SetActive(true);
        InvokeRepeating(nameof(PlayCallingSound), 1f, 2f);
        Invoke(nameof(NPCTakeCall), 10f);
    }

    private void NPCTakeCall()
    {
        callTaken.TryRaiseEvent();
        objectiveCallSomeone.TryRaiseEvent();
        callingUI.SetActive(false);
        ongoingCallUI.SetActive(true);
        callerName2.text = callerName;
        CancelInvoke(nameof(PlayCallingSound));
        InvokeRepeating(nameof(AddTime), 0f, 1f);
    }

    private void AcceptCall()
    {
        callAccepted.TryRaiseEvent();
        objectiveAnswerPhone.TryRaiseEvent();
        comingCallUI.SetActive(false);
        ongoingCallUI.SetActive(true);
        callerName2.text = callerName;
        callTimeSeconds = 0;
        callTimeMinutes = 0;
        InvokeRepeating(nameof(AddTime), 0f, 1f);
    }

    private void EndCall()
    {
        nextObjectives[objective].TryRaiseEvent();
        objective++;
        ongoingCallUI.SetActive(false);
        CancelInvoke(nameof(AddTime));
    }

    private void StopCalling()
    {
        callingUI.SetActive(false);
    }

    private void AddTime()
    {
        callTimeSeconds += 1;
        if (callTimeSeconds == 60)
        {
            callTimeMinutes += 1;
            callTimeSeconds = 0;
        }
        if (callTimeSeconds <= 9 && callTimeMinutes == 0)
        {
            callTime.text = "00:0" + callTimeSeconds;
        }
        else if (callTimeSeconds >= 10 && callTimeMinutes == 0)
        {
            callTime.text = "00:" + callTimeSeconds;
        }
        else if (callTimeSeconds <= 9 && callTimeMinutes <= 9)
        {
            callTime.text = "0" + callTimeMinutes + ":0" + callTimeSeconds;
        }
        else if (callTimeSeconds >= 10 && callTimeMinutes >= 10)
        {
            callTime.text = callTimeMinutes + ":" + callTimeSeconds;
        }
    }

    private void PlayCallingSound()
    {
        callingSound.Play();
    }
}