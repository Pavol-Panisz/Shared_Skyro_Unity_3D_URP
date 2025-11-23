using Game.Notifications;
using UnityEngine;

namespace Game.UI
{
    public class NotificationView : MonoBehaviour, INotificationView
    {
        [SerializeField] private Transform notificationContainer;
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private float lifetime = 2.5f;

        public void Show(string message, NotificationType type)
        {
            GameObject notifObject = Instantiate(notificationPrefab, notificationContainer);
            var ui = notifObject.GetComponent<NotificationUIElement>();
            ui.SetMessage(message, type);
            StartCoroutine(DestroyLater(notifObject, lifetime));
        }

        private System.Collections.IEnumerator DestroyLater(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(obj);
        }
    }
}