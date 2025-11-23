using Game.Notifications;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class NotificationUIElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Image background;
        [SerializeField] private Color infoColor = new(.3f, .7f, 1f, .8f);
        [SerializeField] private Color warningColor = new(1f, 0.8f, 0.3f, 0.8f);
        [SerializeField] private Color errorColor = new(1f, 0.3f, 0.3f, 0.8f);

        public void SetMessage(string message, NotificationType type)
        {
            messageText.text = message;

            background.color = type switch
            {
                NotificationType.Info => infoColor,
                NotificationType.Warning => warningColor,
                NotificationType.Error => errorColor,
                _ => background.color
            };

            StartCoroutine(FadeIn());
        }

        private System.Collections.IEnumerator FadeIn()
        {
            if(!TryGetComponent(out CanvasGroup cg))
                yield break;
            
            cg.alpha = 0f;
            float t = 0f;

            while(t < 1f)
            {
                cg.alpha = Mathf.Lerp(0, 1, t);
                t += Time.deltaTime * 4f;
                yield return null;
            }

            cg.alpha = 1f;
        }
    }
}