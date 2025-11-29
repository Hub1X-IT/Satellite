using System;
using System.Collections;
using DialogSystem.Runtime.Core;
using DialogSystem.Runtime.Interfaces;
using DialogSystem.Runtime.Models;
using UnityEngine;

public class DialogManagerHelper : MonoBehaviour
{
    private const string DialogEndActionId = "dialog_end";

    private void Awake()
    {
        DialogManager.Instance.onDialogExit += OnDialogExit;
    }

    private void OnDialogExit()
    {
        
    }
}