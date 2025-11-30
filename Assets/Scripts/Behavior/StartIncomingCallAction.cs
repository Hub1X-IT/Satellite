using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Start Incoming Call", story: "Start incoming call from [Contact]", category: "Action/Phonecall", id: "69095e96a357de04d1d1114cc9356d79")]
public partial class StartIncomingCallAction : Action
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

        call = PhonecallManager.Instance.StartIncomingCall(Contact.Value, CanBeEnded.Value);

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

