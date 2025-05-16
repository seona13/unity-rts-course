using GameDevTV.RTS.Units;
using UnityEngine;


namespace GameDevTV.RTS.Commands
{
    [CreateAssetMenu(fileName = "Move Action", menuName = "AI/Actions/Move", order = 100)]
    public class MoveCommand : ActionBase
    {
        [SerializeField] float radiusMultiplier = 3.5f;
        int unitsOnLayer;
        int maxUnitsOnLayer;
        float circleRadius;
        float radialOffset;


        public override bool CanHandle(CommandContext context)
        {
            return context.Commandable is AbstractUnit;
        }

        public override void Handle(CommandContext context)
        {
            AbstractUnit unit = (AbstractUnit)context.Commandable;

            if (context.UnitIndex == 0)
            {
                unitsOnLayer = 0;
                maxUnitsOnLayer = 1;
                circleRadius = 0;
                radialOffset = 0;
            }

            Vector3 targetPosition = new(
                context.Hit.point.x + circleRadius * Mathf.Cos(radialOffset * unitsOnLayer),
                context.Hit.point.y,
                context.Hit.point.z + circleRadius * Mathf.Sin(radialOffset * unitsOnLayer)
                );

            unit.MoveTo(targetPosition);
            unitsOnLayer++;

            if (unitsOnLayer >= maxUnitsOnLayer)
            {
                unitsOnLayer = 0;
                circleRadius += unit.AgentRadius * radiusMultiplier;
                maxUnitsOnLayer = Mathf.FloorToInt(2 * Mathf.PI * circleRadius / (unit.AgentRadius * 2));
                radialOffset = 2 * Mathf.PI / maxUnitsOnLayer;
            }
        }
    }
}