using UnityEngine;


namespace GameDevTV.RTS.Environment
{
    public class GatherableSupply : MonoBehaviour, IGatherable
    {
        [field: SerializeField] public SupplySO Supply { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public bool IsBusy { get; private set; }


        void Start()
        {
            Amount = Supply.MaxAmount;
        }


        public bool BeginGather()
        {
            if (IsBusy)
            {
                return false;
            }

            IsBusy = true;
            return true;
        }


        public int EndGather()
        {
            IsBusy = false;
            int amountGathered = Mathf.Min(Supply.AmountPerGather, Amount);
            Amount -= amountGathered;

            if (Amount <= 0)
            {
                Destroy(gameObject); // All supplies have been gathered
            }

            return amountGathered;
        }
    }
}