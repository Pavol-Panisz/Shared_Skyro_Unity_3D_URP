namespace Game.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationView _view;

        public NotificationService(INotificationView view)
        {
            _view = view;
        }

        public void Notify(string message, NotificationType type = NotificationType.Error)
        {
            _view.Show(message, type);
        }
    }
}