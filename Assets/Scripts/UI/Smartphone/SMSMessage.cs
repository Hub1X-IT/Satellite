using System;
using UnityEngine;

[Serializable]
public class SMSMessage
{
    public SMSMessage(string content, string title = null, string sender = "<Unknown>", string date = "--:--", string description = null)
    {
        this.title = title ?? ShortenedText(content, 20);
        this.content = content;
        this.sender = sender;
        this.date = date;
        this.description = description ?? ShortenedText(content, 55);
    }

    public SMSMessage Copy(string title = null, string content = null, string sender = null, string date = null, string description = null)
    {
        return new SMSMessage(title ?? Title, content ?? Content, sender ?? Sender, date ?? Date, description ?? Description);
    }

    [SerializeField] private string title;
    [SerializeField, TextArea(3, 5)] private string content;
    [SerializeField] private string sender;
    [SerializeField] private string date;
    [SerializeField] private string description;

    public string Title => title;
    public string Content => content;
    public string Sender => sender;
    public string Date => date;
    public string Description => description;


    private string ShortenedText(string text, int cutoff) {
        if (text.Length <= cutoff) return text;
        return text[..cutoff].Replace(System.Environment.NewLine, " ") + "...";
    }
}
