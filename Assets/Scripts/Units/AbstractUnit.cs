using GameDevTV.RTS.EventBus;
using GameDevTV.RTS.Events;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


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
            graphAgent.SetVariableValue("Command", UnitCommands.Stop);
        }


        protected override void Start()
        {
            base.Start();
            Bus<UnitSpawnEvent>.Raise(new UnitSpawnEvent(this));
        }


        public void MoveTo(Vector3 position)
        {
            graphAgent.SetVariableValue("TargetLocation", position);
            graphAgent.SetVariableValue("Command", UnitCommands.Move);
        }


        public void Stop()
        {
            graphAgent.SetVariableValue("Command", UnitCommands.Stop);
        }
    }
}