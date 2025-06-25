using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;


namespace GameDevTV.RTS.Behaviour
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Stop Agent", story: "[Agent] stops moving", category: "Action/Navigation", id: "c43ee07ccc85f4fcb3d05dcd5ecb9d37")]
    public partial class StopAgentAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;

        protected override Status OnStart()
        {
            if (Agent.Value.TryGetComponent(out NavMeshAgent agent))
            {
                agent.ResetPath();
                return Status.Success;
            }
            return Status.Failure;
        }
    }
}