using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Delete Waypoint", story: "Delete [Waypoint]", category: "Action", id: "93b2ed245a61e7e0249d65276e886980")]
public partial class DeleteWaypointAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Waypoint;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;

    protected override Status OnStart()
    {
        Waypoints.Value.RemoveAll(w => w == Waypoint.Value);
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

