using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevTV.RTS.Units
{
    public class BaseBuilding : AbstractCommandable
    {
        const int MAX_QUEUE_SIZE = 5;

        public int QueueSize => buildingQueue.Count;
        [field: SerializeField] public float CurrentQueueStartTime {  get; private set; }
        [field: SerializeField] public UnitSO BuildingUnit { get; private set; }

        public delegate void QueueUpdatedEvent(UnitSO[] unitsInQueue);
        public event QueueUpdatedEvent OnQueueUpdated;

        Queue<UnitSO> buildingQueue = new(MAX_QUEUE_SIZE);


        public void BuildUnit(UnitSO unit)
        {
            if (buildingQueue.Count == MAX_QUEUE_SIZE)
            {
                Debug.LogError("BuildUnit called when the queue was already full! This is not supported!");
                return;
            }

            buildingQueue.Enqueue(unit);
            if (buildingQueue.Count == 1)
            {
                StartCoroutine(DoBuildUnits());
            }
            else
            {
                OnQueueUpdated?.Invoke(buildingQueue.ToArray());
            }
        }


        IEnumerator DoBuildUnits()
        {
            while (buildingQueue.Count > 0)
            {
                BuildingUnit = buildingQueue.Peek();
                CurrentQueueStartTime = Time.time;
                OnQueueUpdated?.Invoke(buildingQueue.ToArray());

                yield return new WaitForSeconds(BuildingUnit.BuildTime);

                Instantiate(BuildingUnit.Prefab, transform.position, Quaternion.identity);
                buildingQueue.Dequeue();
            }

            OnQueueUpdated?.Invoke(buildingQueue.ToArray());
        }
    }
}