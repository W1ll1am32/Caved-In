using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Save Location", story: "Save [Target] Location", category: "Action", id: "5eaf0916b1607480e29a1f41eb089b77")]
public partial class SaveLocationAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

    GameObject targetObject;

    protected override Status OnStart()
    {
        TargetLocation.SetValueWithoutNotify(Target.Value.GetComponent<Transform>().position);
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

