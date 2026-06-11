using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Send Text Message", story: "Send [TextMessage] text message", category: "Action/Text Message", id: "fb56620e74b6f6ed79f1e933faf036da")]
public partial class SendTextMessageAction : Action
{
    [SerializeReference] public BlackboardVariable<SMSMessageSO> TextMessage;
    protected override Status OnStart()
    {
        if (TextMessage == null)
        {
            return Status.Failure;
        }

        SMSManager.Instance.SendMessageSO(TextMessage.Value);

        return Status.Success;
    }
}

