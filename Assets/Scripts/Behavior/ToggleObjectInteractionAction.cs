using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Toggle Object Interaction", story: "Toggle interaction on [InteractableObject] [NewState]", category: "Action/Interaction", id: "8f0324ce7b1109610a1fd5208c0686fd")]
public partial class ToggleObjectInteractionAction : Action
{
    [SerializeReference] public BlackboardVariable<Interactable> InteractableObject;
    [SerializeReference] public BlackboardVariable<bool> NewState;

    protected override Status OnStart()
    {
        if (InteractableObject.Value == null)
        {
            return Status.Failure;
        }

        InteractableObject.Value.SetObjectInteractable(NewState.Value);

        return Status.Success;
    }
}

