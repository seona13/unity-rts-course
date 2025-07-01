using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

namespace GameDevTV.RTS.Behaviour
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Move to Target GameObject", story: "[Agent] moves to [TargetGameObject]", category: "Action/Navigation", id: "060a3cc5502a5b7077e820ccb0de7dd0")]
    public partial class MoveToTargetGameObjectAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;
        [SerializeReference] public BlackboardVariable<GameObject> TargetGameObject;

        NavMeshAgent agent;


        protected override Status OnStart()
        {
            if (Agent.Value.TryGetComponent(out agent) == false)
            {
                // We don't have a NavMeshAgent, so can't move anyway.
                return Status.Failure;
            }

            Vector3 targetPosition = GetTargetPosition();

            if (Vector3.Distance(agent.transform.position, targetPosition) <= agent.stoppingDistance)
            {
                // We're already there!
                return Status.Success;
            }

            agent.SetDestination(targetPosition);

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


        Vector3 GetTargetPosition()
        {
            Vector3 targetPosition;
            if (TargetGameObject.Value.TryGetComponent(out Collider collider))
            {
                targetPosition = collider.ClosestPoint(agent.transform.position);
            }
            else
            {
                targetPosition = TargetGameObject.Value.transform.position;
            }

            return targetPosition;
        }
    }
}