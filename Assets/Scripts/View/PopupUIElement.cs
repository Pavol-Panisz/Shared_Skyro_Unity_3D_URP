using Game.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PopupUIElement : MonoBehaviour, IPopupView
    {
        [SerializeField] private Image inlineImg;
        [SerializeField] private TMP_Text actionBtnText;
        [SerializeField] private Color addColor;
        [SerializeField] private Color removeColor;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField amountInputField;

        private PopupVersion currentVersion;

        public void SetVersion(PopupVersion version)
        {
            currentVersion = version;

            inlineImg.color = version == PopupVersion.ADD ? addColor : removeColor;
            actionBtnText.text = version == PopupVersion.ADD ? "Add" : "Remove";

            nameInputField.text = "";
            amountInputField.text = "";
        }

        public void SetEnabled(bool enabled)
        {
            gameObject.SetActive(enabled);
        }

        public string GetName() => nameInputField.text;
        public string GetAmount() => amountInputField.text;
        public PopupVersion GetVersion() => currentVersion;
    }
}