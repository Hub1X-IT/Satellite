using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetCanEndCall", story: "Allow player to end current call: [CanEndCall]", category: "Action", id: "32520235bf73e489a378255265c0bd4d")]
public partial class SetCanEndCallAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanEndCall;

    protected override Status OnStart()
    {
        if (PhonecallManager.Instance.CurrentCall == null)
        {
            return Status.Failure;
        }

        PhonecallManager.Instance.SetCanEndCall(CanEndCall.Value);
        return Status.Success;
    }
}
