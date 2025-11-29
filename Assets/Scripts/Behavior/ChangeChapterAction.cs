using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Change Chapter", story: "Set current chapter to [Chapter]", category: "Action/Objectives", id: "9637dee700564bff29c47d5ecc560ca0")]
public partial class ChangeChapterAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Chapter;
    protected override Status OnStart()
    {
        ObjectivesManager.Instance.SetChapter(Chapter.Value);
        
        return Status.Success;
    }
}

