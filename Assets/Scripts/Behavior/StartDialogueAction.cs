using DialogSystem.Runtime.Models;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using DialogSystem.Runtime.Core;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Start Dialogue", story: "Start dialogue [Dialog]", category: "Action/Dialogue", id: "57e2f0253f83b6cd1e166958810d317e")]
public partial class StartDialogueAction : Action
{
    [SerializeReference] public BlackboardVariable<DialogGraph> Dialog;
    [SerializeReference] public BlackboardVariable<bool> WaitForDialogEnd;

    private bool isDialogEnded = false;

    protected override Status OnStart()
    {
        if (Dialog.Value == null)
        {
            return Status.Failure;
        }

        DialogManager.Instance.StartDialog(Dialog.Value);

        if (WaitForDialogEnd)
        {
            isDialogEnded = false;
            DialogManager.Instance.onDialogExit += OnDialogExit;
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }

    protected override Status OnUpdate()
    {
        if (isDialogEnded)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        DialogManager.Instance.onDialogExit -= OnDialogExit;
    }

    private void OnDialogExit()
    {
        isDialogEnded = true;
    }
}

