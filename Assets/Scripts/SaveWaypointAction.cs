using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Save Waypoint", story: "Save [Waypoint]", category: "Action", id: "bf9624a28f038f512a757e1758c8091b")]
public partial class SaveWaypointAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Waypoint;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;

    protected override Status OnStart()
    {
        Waypoints.Value.Add(Waypoint.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

