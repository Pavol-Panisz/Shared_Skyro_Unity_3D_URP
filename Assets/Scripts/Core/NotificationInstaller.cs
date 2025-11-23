using Game.Notifications;
using Game.UI;

namespace Game.Installers
{
    public class NotificationInstaller
    {
        public INotificationService Install(INotificationView view)
        {
            return new NotificationService(view);
        }
    }
}