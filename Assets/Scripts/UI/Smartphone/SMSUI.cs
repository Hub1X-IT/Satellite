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

    private int unreadCount = 1;

    private void RefreshUnreadCount()
    {
        ping.Object.SetActive(unreadCount != 0);
        ping.UnreadCount.text = unreadCount.ToString();
    }

    private void Awake()
    {
        RefreshUnreadCount();
        SingleSMSController.OnFirstOpen += () => { unreadCount--; RefreshUnreadCount(); };
    }
}