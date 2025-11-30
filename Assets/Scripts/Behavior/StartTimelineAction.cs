using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Playables;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Start Timeline", story: "Start timeline [Timeline]", category: "Action/Timeline", id: "3716aae501e4e8f3722768b66deb4f85")]
public partial class StartTimelineAction : Action
{
    [SerializeReference] public BlackboardVariable<PlayableDirector> Timeline;

    protected override Status OnStart()
    {
        Timeline.Value.Play();

        return Status.Success;
    }
}

