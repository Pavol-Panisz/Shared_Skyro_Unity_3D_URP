using System.Globalization;
using Game.Items;
using Game.Notifications;

namespace Game.Popup
{
    public class PopupService : IPopupService
    {
        private readonly IPopupView _view;
        private readonly IItemsService _items;
        private readonly INotificationService _notifications;

        public PopupService(
            IPopupView view,
            IItemsService itemsService,
            INotificationService notificationService
        )
        {
            _view = view;
            _items = itemsService;
            _notifications = notificationService;
        }

        public void OnAddButton()
        {
            _view.SetVersion(PopupVersion.ADD);
            _view.SetEnabled(true);
        }

        public void OnRemoveButton()
        {
            _view.SetVersion(PopupVersion.REMOVE);
            _view.SetEnabled(true);
        }

        public void OnClose()
        {
            _view.SetEnabled(false);
        }

        public void OnAction()
        {
            string name = _view.GetName();
            string amountText = _view.GetAmount();
            var version = _view.GetVersion();

            if (string.IsNullOrWhiteSpace(name))
            {
                _notifications.Notify("Item name cannot be empty", NotificationType.Warning);
                return;
            }

            if (!float.TryParse(amountText, NumberStyles.Float, CultureInfo.InvariantCulture, out float amount))
            {
                _notifications.Notify("Invalid amount entered", NotificationType.Error);
                return;
            }

            if (amount < 0)
            {
                _notifications.Notify("Negative amount is not allowed", NotificationType.Warning);
                return;
            }

            if (_items.Exists(name))
            {
                float current = _items.Get(name);

                switch (version)
                {
                    case PopupVersion.ADD:
                        _items.Change(name, current + amount);
                        break;

                    case PopupVersion.REMOVE:
                        if (amount >= current)
                            _items.Remove(name);
                        else
                            _items.Change(name, current - amount);
                        break;
                }
            }
            else
            {
                if (version == PopupVersion.ADD)
                    _items.Add(name, amount);
            }

            _view.SetEnabled(false);
        }
    }
}