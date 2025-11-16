using System;
using System.Collections;
using DialogSystem.Runtime.Core;
using DialogSystem.Runtime.Interfaces;
using DialogSystem.Runtime.Models;
using UnityEngine;

[RequireComponent(typeof(DialogManager))]
public class DialogManagerHelper : MonoBehaviour, IActionHandler
{
    [Serializable]
    private class DialogStartData
    {
        public DialogGraph dialogGraph;
        public GameEventSO[] triggerEvents;
        public GameEventSO[] dialogEndEvents;
    }

    private DialogManager dialogManager;

    [SerializeField]
    private DialogStartData[] dialogsStartData;

    private void Awake()
    {
        dialogManager = GetComponent<DialogManager>();

        foreach (var dialogStartData in dialogsStartData)
        {
            foreach (var gameEvent in dialogStartData.triggerEvents)
            {
                gameEvent.EventRaised += () =>
                {
                    dialogManager.StartDialog(dialogStartData.dialogGraph);
                };
            }
            foreach (var gameEvent in dialogStartData.dialogEndEvents)
            {
                dialogManager.onDialogExit += () =>
                {
                    gameEvent.TryRaiseEvent();
                };
            }
        }
    }

    public bool CanHandle(string actionId)
    {
        foreach (var dialogStartData in dialogsStartData)
        {
            if (dialogStartData.dialogGraph.actionNodes.Exists(node => node.actionId == actionId))
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator Handle(string actionId, string payloadJson)
    {
        throw new NotImplementedException();
    }
}