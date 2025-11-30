using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Allow Call End", story: "Allow player to end current call", category: "Action", id: "f66cebae45817d64661aa7369a8ceff1")]
public partial class AllowCallEndAction : Action
{

    protected override Status OnStart()
    {
        if (PhonecallManager.Instance.CurrentCall == null)
        {
            return Status.Failure;
        }

        PhonecallManager.Instance.SetCanEndCall();
        return Status.Success;
    }
}

