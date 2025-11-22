using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SMSViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text time;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text sender;

    private SMSMessage currentMessage;

    public void SetCurrent(SMSMessage message)
    {
        currentMessage = message;
        title.text = message.Title;
        time.text = message.Date;
        text.text = message.Content;
        sender.text = message.Sender;
    }
}