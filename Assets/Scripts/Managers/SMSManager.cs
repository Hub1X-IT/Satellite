using System;
using System.Collections.Generic;
using UnityEngine;

public class SMSManager : MonoBehaviour
{
    [Serializable]
    private class EventBoundMessage
    {
        public GameEventSO activationEvent;
        public SMSMessageSO message;
        public GameEventSO openedEvent;
    }

    [SerializeField]
    private SMSUI smsUI;

    [SerializeField]
    private SmartphoneMenuUI menu;

    [SerializeField]
    private List<EventBoundMessage> messages;

    private int unreadCount;

    private void Awake()
    {
        foreach (var item in messages)
        {
            item.activationEvent.EventRaised += () => {
                SendMessage(item.message.GetMessage(), () => {
                    item.openedEvent?.TryRaiseEvent();
                });
            };
        }
    }

    private void DecreaseCount()
    {
        unreadCount--;
        menu.SetNotificationCount("messages", unreadCount);
    }

    public void SendMessage(SMSMessage message, Action onFirstOpen = null)
    {
        unreadCount++;
        menu.SetNotificationCount("messages", unreadCount);
        smsUI.SendMessage(message, onFirstOpen + DecreaseCount);
    }
}
