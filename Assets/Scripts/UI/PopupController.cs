using System.Globalization;
using Game.Core;
using Game.UI;
using UnityEngine;

namespace Game.UI
{
    public class PopupController : MonoBehaviour
    {
        [SerializeField] private PopupUIElement popupUI;

        public void OnAddButtonPressed()
        {
            popupUI.SetVersion(PopupUIElement.PopupVersion.ADD);
            popupUI.SetEnabled(true);
        }

        public void OnRemoveButtonPressed()
        {
            popupUI.SetVersion(PopupUIElement.PopupVersion.REMOVE);
            popupUI.SetEnabled(true);
        }

        public void OnCloseButtonPressed()
        {
            popupUI.SetEnabled(false);
        }

        public void OnActionButtonPressed()
        {
            string name = popupUI.GetName();
            string amountText = popupUI.GetAmount();

            if (string.IsNullOrWhiteSpace(name))
            {
                NotificationManager.Notify("Item name cannot be empty", NotificationManager.NotificationType.Warning);
                return;
            }

            if (!float.TryParse(amountText, NumberStyles.Float, CultureInfo.InvariantCulture, out float amount))
            {
                NotificationManager.Notify("Invalid amount entered", NotificationManager.NotificationType.Error);
                return;
            }

            if(amount < 0)
            {
                NotificationManager.Notify("Negative amount is not allowed", NotificationManager.NotificationType.Warning);
                return;
            }

            ItemsManager items = Manager.Instance.Items;
            var version = popupUI.GetVersion();

            if (items.ExistsItem(name))
            {
                if (!float.TryParse(items.GetItem(name), NumberStyles.Float, CultureInfo.InvariantCulture, out float current))
                {
                    NotificationManager.Notify("Failed to parse existing item amount", NotificationManager.NotificationType.Error);
                    return;
                }

                switch (version)
                {
                    case PopupUIElement.PopupVersion.ADD:
                        items.ChangeItem(name, current + amount);
                        break;

                    case PopupUIElement.PopupVersion.REMOVE:
                        if (amount >= current)
                            items.RemoveItem(name);
                        else
                            items.ChangeItem(name, current - amount);
                        break;
                }
            }
            else if (version == PopupUIElement.PopupVersion.ADD)
            {
                items.AddItem(name, amount.ToString(CultureInfo.InvariantCulture));
            }

            popupUI.SetEnabled(false);
        }
    }
}

