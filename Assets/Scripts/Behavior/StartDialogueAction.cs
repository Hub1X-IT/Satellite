using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartDialogue", story: "Start dialogue [DialogueNodeName]", category: "Dialogue", id: "d7bb08a74a71398cbb69b56903e808f2")]
public partial class StartDialogueAction : Action
{
    [SerializeReference] public BlackboardVariable<string> DialogueNodeName;

    [SerializeReference] public BlackboardVariable<bool> WaitForDialogueEnd = (BlackboardVariable<bool>)true;

    protected override Status OnStart()
    {
        DialogueManager.Instance.StartDialogue(DialogueNodeName);

        if (WaitForDialogueEnd)
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }

    protected override Status OnUpdate()
    {
        if (DialogueManager.Instance.IsDialogueRunning)
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }
}

