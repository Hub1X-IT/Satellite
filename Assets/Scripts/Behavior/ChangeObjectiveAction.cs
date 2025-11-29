using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Change Objective", story: "Set current objective to [Objective]", category: "Action/Objectives", id: "1fb6ab1303618b9653de237801287d50")]
public partial class ChangeObjectiveAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Objective;
    protected override Status OnStart()
    {
        ObjectivesManager.Instance.SetObjective(Objective.Value);

        return Status.Success;
    }
}

