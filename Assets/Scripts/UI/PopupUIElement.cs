using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupUIElement : MonoBehaviour
{
    [SerializeField] private Image inlineImg;
    [SerializeField] private TMP_Text actionBtnText;
    [SerializeField] private Color addColor;
    [SerializeField] private Color removeColor;
    [SerializeField] private TMP_Text nameInputText;
    [SerializeField] private TMP_Text amountInputText;

    public enum PopupVersion
    {
        ADD,
        REMOVE
    };

    private PopupVersion currentVersion;

    public void SetVersion(PopupVersion @version)
    {
        switch (@version)
        {
            case PopupVersion.ADD:
                inlineImg.color = addColor;
                actionBtnText.text = "Add";
                nameInputText.text = "";
                amountInputText.text = "";
                currentVersion = PopupVersion.ADD;
                break;

            case PopupVersion.REMOVE:
                inlineImg.color = removeColor;
                actionBtnText.text = "Remove";
                nameInputText.text = "";
                amountInputText.text = "";
                currentVersion = PopupVersion.REMOVE;
                break;

            default:
                break;
        }
    }

    public void SetEnabled(bool @enabled)
    {
        gameObject.SetActive(@enabled);
    }

    public string GetName()
    {
        return nameInputText.text;
    }

    public string GetAmount()
    {
        return amountInputText.text;
    }
    
    public PopupVersion GetVersion()
    {
        return currentVersion;
    }
}
