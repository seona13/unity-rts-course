using GameDevTV.RTS.Units;
using UnityEngine;


namespace GameDevTV.RTS.Commands
{
    [CreateAssetMenu(fileName = "Move Action", menuName = "AI/Actions/Move", order = 100)]
    public class MoveCommand : ActionBase
    {
        public override bool CanHandle(AbstractCommandable commandable, RaycastHit hit)
        {
            return commandable is IMovable;
        }

        public override void Handle(AbstractCommandable commandable, RaycastHit hit)
        {
            IMovable movable = (IMovable)commandable;
            movable.MoveTo(hit.point);
        }
    }
}