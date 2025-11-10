using System.Collections;
using UnityEngine;

namespace Game.UI
{
    public class NotificationManager : MonoBehaviour
    {
        public static NotificationManager Instance { get; private set; }

        [SerializeField] private Transform notificationContainer;
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private float lifetime = 2.5f;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public static void Notify(string message, NotificationType type = NotificationType.Error)
        {
            if (Instance == null) return;

            Instance.SpawnNotification(message, type);
        }

        private void SpawnNotification(string message, NotificationType type)
        {
            GameObject notifObj = Instantiate(notificationPrefab, notificationContainer);
            var notif = notifObj.GetComponent<NotificationUIElement>();
            notif.SetMessage(message, type);
            StartCoroutine(DestroyAfterDelay(notifObj, lifetime));
        }
        
        private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(obj);
        }

        public enum NotificationType
        {
            Info,
            Warning,
            Error
        }
    }
}

