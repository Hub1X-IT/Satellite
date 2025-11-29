using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Raise Game Event", story: "Raise [GameEvent]", category: "Action/GameEvents", id: "d42e27d732991ff46d6341d738396e1f")]
public partial class RaiseGameEventAction : Action
{
    [SerializeReference] public BlackboardVariable<GameEventSO> GameEvent;
    protected override Status OnStart()
    {
        if (GameEvent == null)
        {
            return Status.Failure;
        }
        
        GameEvent.Value.TryRaiseEvent();
        return Status.Success;
    }
}

