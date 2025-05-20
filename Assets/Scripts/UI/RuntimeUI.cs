using GameDevTV.RTS.EventBus;
using GameDevTV.RTS.Events;
using GameDevTV.RTS.UI.Containers;
using GameDevTV.RTS.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GameDevTV.RTS.UI
{
    public class RuntimeUI : MonoBehaviour
    {
        [SerializeField] ActionsUI actionsUI;
        [SerializeField] BuildingBuildingUI buildingBuildingUI;

        HashSet<AbstractCommandable> selectedUnits = new(12);


        void Awake()
        {
            Bus<UnitSelectedEvent>.OnEvent += HandleUnitSelected;
            Bus<UnitDeselectedEvent>.OnEvent += HandleUnitDeselected;
        }


        void Start()
        {
            actionsUI.Disable();
            buildingBuildingUI.Disable();
        }


        void OnDestroy()
        {
            Bus<UnitSelectedEvent>.OnEvent -= HandleUnitSelected;
            Bus<UnitDeselectedEvent>.OnEvent -= HandleUnitDeselected;
        }


        void HandleUnitSelected(UnitSelectedEvent evt)
        {
            if (evt.Unit is AbstractCommandable commandable)
            {
                selectedUnits.Add(commandable);
                actionsUI.EnableFor(selectedUnits);
            }

            if (selectedUnits.Count == 1 && evt.Unit is BaseBuilding building)
            {
                buildingBuildingUI.EnableFor(building);
            }
        }

        void HandleUnitDeselected(UnitDeselectedEvent evt) 
        {
            if (evt.Unit is AbstractCommandable commandable)
            {
                selectedUnits.Remove(commandable);

                if (selectedUnits.Count > 0) // some units are still selected
                {
                    actionsUI.EnableFor(selectedUnits);

                    // Is the only thing selected a building?
                    if (selectedUnits.Count == 1 && selectedUnits.First() is BaseBuilding building)
                    {
                        buildingBuildingUI.EnableFor(building);
                    }
                    else
                    {
                        buildingBuildingUI.Disable();
                    }
                }
                else
                {
                    actionsUI.Disable();
                    buildingBuildingUI.Disable();
                }
            }
        }
    }
}