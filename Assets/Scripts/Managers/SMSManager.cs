using System;
using System.Collections.Generic;
using UnityEngine;

public class SMSManager : MonoBehaviour
{
    public static SMSManager Instance { get; private set; }

    [SerializeField]
    private SMSUI smsUI;

    [SerializeField]
    private SmartphoneMenuUI menu;

    private List<SMSMessage> currentMessages;

    private int unreadCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(SMSManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentMessages = new();
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
        currentMessages.Add(message);
    }

    public void SendMessageSO(SMSMessageSO messageSO)
    {
        SendMessage(messageSO.GetMessage(), () =>
        {
            messageSO.OnFirstOpenedGameEvent?.TryRaiseEvent();
        });
    }
}
