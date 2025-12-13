using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "End Ongoing Call", story: "End current ongoing call", category: "Action", id: "a9384edb46560a1f49ccc0a69cf98077")]
public partial class EndOngoingCallAction : Action
{
    protected override Status OnStart()
    {
        if (PhonecallManager.Instance.TryEndOngoingCall())
        {
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }
    }
}

