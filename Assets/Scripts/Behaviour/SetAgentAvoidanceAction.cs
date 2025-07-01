using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

namespace GameDevTV.RTS.Behaviour
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set Agent Avoidance", story: "Set [Agent] avoidance quality to [AvoidanceQuality]", category: "Action/Navigation", id: "c15fb6e951432f355944f22417ce7990")]
    public partial class SetAgentAvoidanceAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;
        [SerializeReference] public BlackboardVariable<int> AvoidanceQuality;


        protected override Status OnStart()
        {
            if (Agent.Value.TryGetComponent(out NavMeshAgent agent) == false
                || AvoidanceQuality > 4
                || AvoidanceQuality < 0)
            {
                // We don't have a NavMeshAgent, or are setting an illegal avoidance quality.
                return Status.Failure;
            }

            agent.obstacleAvoidanceType = (ObstacleAvoidanceType)AvoidanceQuality.Value;
            return Status.Success;
        }
    }
}