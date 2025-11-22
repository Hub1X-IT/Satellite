using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SMSUI : MonoBehaviour
{
    [Serializable]
    public class PingCircle
    {
        public GameObject Object;
        public TMP_Text UnreadCount;
    }
    
    [SerializeField]
    private PingCircle ping;

    [SerializeField]
    private SMSViewController messageView;

    [SerializeField]
    private GameObject messagesList;

    [SerializeField]
    private Transform messagesListContent;

    [SerializeField]
    private GameObject smsPrefab;

    private int unreadCount; 

    private void RefreshUnreadCount()
    {
        ping.Object.SetActive(unreadCount != 0);
        ping.UnreadCount.text = unreadCount.ToString();
    }

    private void DecreaseUnreadCount()
    {
        unreadCount--;
        RefreshUnreadCount();
    }

    private void Awake()
    {
        RefreshUnreadCount();
    }


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
        single.Initialize(this, message, onFirstOpen + DecreaseUnreadCount);
        unreadCount++;
        RefreshUnreadCount();
    }
}