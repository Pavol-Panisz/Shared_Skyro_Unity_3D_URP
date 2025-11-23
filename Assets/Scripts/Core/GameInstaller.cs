using Game.Installers;
using Game.Items;
using Game.Notifications;
using Game.Popup;
using Game.UI;
using UnityEngine;

namespace Game.Core
{
    public class GameInstaller : MonoBehaviour
    {
        [Header("UI Views")]
        [SerializeField] private NotificationView notificationViewMB;
        [SerializeField] private ItemsView itemsViewMB;
        [SerializeField] private PopupUIElement popupViewMB;
        [SerializeField] private PopupControllerView popupControllerMB;

        private INotificationView _notificationView;
        private IItemsView  _itemsView;
        private IPopupView _popupView;
        private IPopupControllerView _popupControllerView;

        private IItemsService _itemsService;
        private INotificationService _notifService;
        private IPopupService _popupService;

        void Awake()
        {
            _notificationView = notificationViewMB;
            _itemsView = itemsViewMB;
            _popupView = popupViewMB;
            _popupControllerView = popupControllerMB;

            _notifService = new NotificationInstaller().Install(_notificationView);
            _itemsService = new ItemsInstaller().Install(_itemsView);
            _popupService = new PopupInstaller().Install(_popupView, _itemsService, _notifService);

            _popupControllerView.Initialize(_popupService);
        }
    }
}