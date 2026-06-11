using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait For GameEvent", story: "Wait until [GameEvent] is raised", category: "Action/GameEvent", id: "d4276d31473c58da035645198a454f84")]
public partial class WaitForGameEventAction : Action
{
    [SerializeReference] public BlackboardVariable<GameEventSO> GameEvent;
    private bool wasRaised;

    protected override Status OnStart()
    {
        if (GameEvent == null)
        {
            return Status.Failure;
        }

        wasRaised = false;
        GameEvent.Value.EventRaised += OnGameEventRaised;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (wasRaised)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        GameEvent.Value.EventRaised -= OnGameEventRaised;
    }

    private void OnGameEventRaised()
    {
        wasRaised = true;
    }
}

