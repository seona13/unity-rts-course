using GameDevTV.RTS.Commands;
using GameDevTV.RTS.EventBus;
using GameDevTV.RTS.Events;
using GameDevTV.RTS.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GameDevTV.RTS.UI
{
    public class ActionsUI : MonoBehaviour
    {
        [SerializeField] UIActionButton[] actionButtons;
        HashSet<AbstractCommandable> selectedUnits = new(12);


        void Awake()
        {
            Bus<UnitSelectedEvent>.OnEvent += HandleUnitSelected;
            Bus<UnitDeselectedEvent>.OnEvent += HandleUnitDeselected;

            foreach (UIActionButton button in actionButtons)
            {
                button.SetIcon(null);
            }
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
                RefreshButtons();
            }
        }

        void HandleUnitDeselected(UnitDeselectedEvent evt)
        {
            if (evt.Unit is AbstractCommandable commandable)
            {
                selectedUnits.Remove(commandable);
                RefreshButtons();
            }
        }


        void RefreshButtons()
        {
            HashSet<ActionBase> availableCommands = new(9);

            foreach (AbstractCommandable commandable in selectedUnits)
            {
                availableCommands.UnionWith(commandable.AvailableCommands);
            }

            for (int i = 0; i < actionButtons.Length; i++)
            {
                ActionBase actionForSlot = availableCommands.Where(action => action.Slot == i).FirstOrDefault();

                if (actionForSlot != null)
                {
                    actionButtons[i].SetIcon(actionForSlot.Icon);
                }
                else
                {
                    actionButtons[i].SetIcon(null);
                }
            }
        }
    }
}