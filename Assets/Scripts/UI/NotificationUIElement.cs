using System.Collections;
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

        public void SetMessage(string message, NotificationManager.NotificationType type)
        {
            messageText.text = message;

            switch (type)
            {
                case NotificationManager.NotificationType.Info:
                    background.color = infoColor;
                    break;
                case NotificationManager.NotificationType.Warning:
                    background.color = warningColor;
                    break;
                case NotificationManager.NotificationType.Error:
                    background.color = errorColor;
                    break;
            }

            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            CanvasGroup cg = GetComponent<CanvasGroup>();
            if (!cg) yield break;

            cg.alpha = 0;
            float t = 0;
            while (t < 1)
            {
                cg.alpha = Mathf.Lerp(0, 1, t);
                t += Time.deltaTime * 4f;
                yield return null;
            }
            cg.alpha = 1;
        }
    }
}

