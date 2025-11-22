using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SMSUI : MonoBehaviour
{
    [SerializeField]
    private SMSViewController messageView;

    [SerializeField]
    private GameObject messagesList;

    [SerializeField]
    private Transform messagesListContent;

    [SerializeField]
    private GameObject smsPrefab;

    public void OpenMessage(SMSMessage message, Action onClose = null)
    {
        bool bringBackList = messagesList.activeSelf;
        messagesList.SetActive(false);
        messageView.SetCurrent(message);
        messageView.GetComponent<EnterableUIObject>().Enable(() => {
            onClose?.Invoke();
            if (bringBackList) messagesList.SetActive(true);
        });
    }

    public void SendMessage(SMSMessage message, Action onFirstOpen = null)
    {
        SingleSMSController single = Instantiate(smsPrefab, messagesListContent).GetComponent<SingleSMSController>();
        single.Initialize(this, message, onFirstOpen);
    }
}