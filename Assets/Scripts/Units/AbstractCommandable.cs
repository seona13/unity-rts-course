using GameDevTV.RTS.Commands;
using GameDevTV.RTS.EventBus;
using GameDevTV.RTS.Events;
using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace GameDevTV.RTS.Units
{
    public abstract class AbstractCommandable : MonoBehaviour, ISelectable
    {
        [SerializeField] DecalProjector decalProjector;
        [SerializeField] UnitSO unitSO;

        [field: SerializeField] public int CurrentHealth { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public ActionBase[] AvailableCommands { get; private set; }


        protected virtual void Start()
        {
            if (unitSO != null)
            {
                CurrentHealth = unitSO.Health;
                MaxHealth = unitSO.Health;
            }
        }


        public void Deselect()
        {
            if (decalProjector != null)
            {
                decalProjector.gameObject.SetActive(false);
            }

            Bus<UnitDeselectedEvent>.Raise(new UnitDeselectedEvent(this));
        }


        public void Select()
        {
            if (decalProjector != null)
            {
                decalProjector.gameObject.SetActive(true);
            }

            Bus<UnitSelectedEvent>.Raise(new UnitSelectedEvent(this));
        }

    }
}