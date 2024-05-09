using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Email {


    public Email(string title, string content) {
        this.title = title;
        this.content = content;
    }


    private string title;
    private string content;


    public string GetTitle() { return title; }

    public string GetContent() { return content; }
}
