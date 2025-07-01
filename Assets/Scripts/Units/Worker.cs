using GameDevTV.RTS.Environment;

namespace GameDevTV.RTS.Units
{
    public class Worker : AbstractUnit
    {
        public void Gather(GatherableSupply supply)
        {
            graphAgent.SetVariableValue("Supply", supply);
            graphAgent.SetVariableValue("TargetGameObject", supply.gameObject);
            graphAgent.SetVariableValue("Command", UnitCommands.Gather);
        }
    }
}