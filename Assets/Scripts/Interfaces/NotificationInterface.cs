namespace Game.Notifications
{
    public interface INotificationService
    {
        void Notify(string message, NotificationType type = NotificationType.Error);
    }
    
    public interface INotificationView
    {
        void Show(string message, NotificationType type);
    }
}