using Game.Items;
using Game.Notifications;

namespace Game.Popup
{
    public class PopupInstaller
    {
        public IPopupService Install(
            IPopupView view,
            IItemsService itemsService,
            INotificationService notificationService)
        {
            return new PopupService(view, itemsService, notificationService);
        }
    }
}
