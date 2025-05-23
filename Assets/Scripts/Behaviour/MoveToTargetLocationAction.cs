using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

namespace GameDevTV.RTS.Behaviour
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Move To Target Location", story: "[Agent] moves to [TargetLocation]", category: "Action/Navigation", id: "d80ac187bce3faed580ab3c3d7a5df44")]
    public partial class MoveToTargetLocationAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;
        [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

        NavMeshAgent agent;


        protected override Status OnStart()
        {
            if (Agent.Value.TryGetComponent(out agent) == false)
            {
                // We don't have a NavMeshAgent, so can't move anyway.
                return Status.Failure;
            }

            if (Vector3.Distance(agent.transform.position, TargetLocation.Value) <= agent.stoppingDistance)
            {
                // We're already there!
                return Status.Success;
            }

            agent.SetDestination(TargetLocation.Value);

            return Status.Running;
        }


        protected override Status OnUpdate()
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // We've arrived!
                return Status.Success;
            }

            // Still travelling...
            return Status.Running;
        }
    }
}