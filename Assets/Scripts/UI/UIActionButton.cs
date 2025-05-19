using GameDevTV.RTS.Commands;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;


namespace GameDevTV.RTS.UI
{
    [RequireComponent(typeof(Button))]
    public class UIActionButton : MonoBehaviour
    {
        [SerializeField] Image icon;

        Button button;


        void Awake()
        {
            button = GetComponent<Button>();
        }


        public void EnableFor(ActionBase action, UnityAction onClick)
        {
            SetIcon(action.Icon);
            button.interactable = true;
            button.onClick.AddListener(onClick);
        }


        public void Disable()
        {
            SetIcon(null);
            button.interactable = false;
            button.onClick.RemoveAllListeners();
        }


        void SetIcon(Sprite icon)
        {
            if (icon == null)
            {
                this.icon.enabled = false;
            }
            else
            {
                this.icon.sprite = icon;
                this.icon.enabled = true;
            }
        }
    }
}