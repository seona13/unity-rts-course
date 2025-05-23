using GameDevTV.RTS.Commands;
using GameDevTV.RTS.Units;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace GameDevTV.RTS.UI.Components
{
    [RequireComponent(typeof(Button))]
    public class UIBuildQueueButton : MonoBehaviour, IUIElement<UnitSO, UnityAction>
    {
        [SerializeField] Image icon;

        Button button;


        void Awake()
        {
            button = GetComponent<Button>();
            Disable();
        }


        public void EnableFor(UnitSO item, UnityAction callback)
        {
            button.onClick.RemoveAllListeners();
            button.interactable = true;
            button.onClick.AddListener(callback);
            icon.gameObject.SetActive(true);
            icon.sprite = item.Icon;
        }


        public void Disable()
        {
            button.interactable = false;
            button.onClick.RemoveAllListeners();
            icon.gameObject.SetActive(false);
        }
    }
}