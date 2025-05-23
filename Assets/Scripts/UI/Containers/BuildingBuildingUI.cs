using GameDevTV.RTS.UI.Components;
using GameDevTV.RTS.Units;
using System.Collections;
using UnityEngine;


namespace GameDevTV.RTS.UI.Containers
{
    public class BuildingBuildingUI : MonoBehaviour, IUIElement<BaseBuilding>
    {
        [SerializeField] UIBuildQueueButton[] unitButtons;
        [SerializeField] ProgressBar progressBar;

        Coroutine buildCoroutine;
        BaseBuilding building;


        public void EnableFor(BaseBuilding item)
        {
            progressBar.SetProgress(0);
            gameObject.SetActive(true);
            building = item;
            building.OnQueueUpdated += HandleQueueUpdated;
            SetUpUnitButtons();

            buildCoroutine = StartCoroutine(UpdateUnitProgress());
        }


        private void SetUpUnitButtons()
        {
            int i = 0;
            for (; i < building.QueueSize; i++)
            {
                int index = i;
                unitButtons[i].EnableFor(building.Queue[i], () => building.CancelBuildingUnit(index));
            }

            for (; i < unitButtons.Length; i++)
            {
                unitButtons[i].Disable();
            }
        }


        public void Disable()
        {
            if (building != null)
            {
                building.OnQueueUpdated -= HandleQueueUpdated;
            }

            gameObject.SetActive(false);
            building = null;
            buildCoroutine = null;
        }


        void HandleQueueUpdated(UnitSO[] unitsInQueue)
        {
            if (unitsInQueue.Length == 1 && buildCoroutine == null)
            {
                buildCoroutine = StartCoroutine(UpdateUnitProgress());
            }

            SetUpUnitButtons();
        }


        IEnumerator UpdateUnitProgress()
        {
            while (building != null && building.QueueSize > 0)
            {
                float startTime = building.CurrentQueueStartTime;
                float endTime = startTime + building.BuildingUnit.BuildTime;

                float progress = Mathf.Clamp01((Time.time - startTime) / (endTime - startTime));
                progressBar.SetProgress(progress);

                yield return null;
            }

            buildCoroutine = null;
        }
    }
}