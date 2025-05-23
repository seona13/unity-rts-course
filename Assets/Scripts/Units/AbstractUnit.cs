using GameDevTV.RTS.EventBus;
using GameDevTV.RTS.Events;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;


namespace GameDevTV.RTS.Units
{
    [RequireComponent(typeof(NavMeshAgent), typeof(BehaviorGraphAgent))]
    public abstract class AbstractUnit : AbstractCommandable, IMovable
    {
        public float AgentRadius => agent.radius;

        NavMeshAgent agent;
        BehaviorGraphAgent graphAgent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            graphAgent = GetComponent<BehaviorGraphAgent>();
        }


        protected override void Start()
        {
            base.Start();
            Bus<UnitSpawnEvent>.Raise(new UnitSpawnEvent(this));
        }


        public void MoveTo(Vector3 position)
        {
            graphAgent.SetVariableValue("TargetLocation", position);
        }
    }
}