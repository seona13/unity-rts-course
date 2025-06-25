using GameDevTV.RTS.Environment;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;


namespace GameDevTV.RTS.Behaviour
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Gather Supplies", story: "[Unit] gathers [Amount] supplies from [GatherableSupplies]", category: "Action/Units", id: "82b8ae10cfff119c25c0dce2197c175f")]
    public partial class GatherSuppliesAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Unit;
        [SerializeReference] public BlackboardVariable<int> Amount;
        [SerializeReference] public BlackboardVariable<GatherableSupply> GatherableSupplies;

        float enterTime;


        protected override Status OnStart()
        {
            enterTime = Time.time;

            GatherableSupplies.Value.BeginGather();
            return Status.Running;
        }


        protected override Status OnUpdate()
        {
            if (GatherableSupplies.Value.Supply.BaseGatherTime + enterTime <= Time.time)
            {
                int amountGathered = GatherableSupplies.Value.EndGather();
                return Status.Success;
            }
            return Status.Running;
        }
    }
}