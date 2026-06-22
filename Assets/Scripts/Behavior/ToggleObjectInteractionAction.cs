using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Toggle Object Interaction", story: "Toggle interaction on [InteractTrigger] [NewState]", category: "Action/Interaction", id: "8f0324ce7b1109610a1fd5208c0686fd")]
public partial class ToggleObjectInteractionAction : Action
{
    [SerializeReference] public BlackboardVariable<InteractionTrigger> InteractTrigger;
    [SerializeReference] public BlackboardVariable<bool> NewState;

    protected override Status OnStart()
    {
        if (InteractTrigger.Value == null)
        {
            return Status.Failure;
        }

        InteractTrigger.Value.SetObjectInteractable(NewState.Value);

        return Status.Success;
    }
}

