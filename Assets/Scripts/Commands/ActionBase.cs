using GameDevTV.RTS.Units;
using UnityEngine;


namespace GameDevTV.RTS.Commands
{
    public abstract class ActionBase : ScriptableObject, ICommand
    {
        public abstract bool CanHandle(AbstractCommandable commandable, RaycastHit hit);
        public abstract void Handle(AbstractCommandable commandable, RaycastHit hit);
    }
}