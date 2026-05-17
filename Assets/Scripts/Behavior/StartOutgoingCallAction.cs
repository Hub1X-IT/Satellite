using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartOutgoingCallAction", story: "Start outgoing call to [Contact]", category: "Action", id: "cd535f34e8a9528fdd75638f65f9d6ca")]
public partial class StartOutgoingCallAction : Action
{
 [SerializeReference] public BlackboardVariable<ContactSO> Contact;
    [SerializeReference] public BlackboardVariable<bool> CanBeEnded = new(true);
    [SerializeReference] public BlackboardVariable<bool> WaitForCallToBeAnswered = new(false);

    private PhonecallManager.Call call;

    protected override Status OnStart()
    {
        if (Contact == null)
        {
            return Status.Failure;
        }

        call = PhonecallManager.Instance.StartOutgoingCall(Contact.Value, CanBeEnded.Value);

        if (call == null)
        {
            return Status.Failure;
        }

        if (WaitForCallToBeAnswered)
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
        if (call.CallType == PhonecallManager.CallType.OngoingCall)
        {
            return Status.Success;
        }

        return Status.Running;
    }
}

