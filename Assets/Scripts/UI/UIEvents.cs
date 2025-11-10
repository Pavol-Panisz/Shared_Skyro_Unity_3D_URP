using System.Globalization;
using UnityEngine;

public class UIEvents : MonoBehaviour
{

    public static void RemoveBtnPressed(PopupUIElement @popupUI)
    {
        @popupUI.SetVersion(PopupUIElement.PopupVersion.REMOVE);
        @popupUI.SetEnabled(true);
    }

    public static void AddBtnPressed(PopupUIElement @popupUI)
    {
        @popupUI.SetVersion(PopupUIElement.PopupVersion.ADD);
        @popupUI.SetEnabled(true);
    }

    public static void ClosePopupBtnPressed(PopupUIElement @popupUI)
    {
        @popupUI.SetEnabled(false);
    }
    
    public static void PopupActionBtnPressed(PopupUIElement @popupUI)
    {
        ItemsManager items = Manager.Instance.GetComponent<ItemsManager>();

        string name = @popupUI.GetName();
        string amount = @popupUI.GetAmount();
        PopupUIElement.PopupVersion version = @popupUI.GetVersion();

        float amountNum;
        if(!float.TryParse(amount, out amountNum))
        {
            Debug.LogError("Failed to parse 'amount' to float");
            return;
        }

        if(items.ExistsItem(name))
        {
            float itemAmount;
            if (!float.TryParse(items.GetItem(name), out itemAmount))
            {
                Debug.LogError("Failed to parse sphereText to integer. 2");
                return;
            }
            
            switch (version)
            {
                case PopupUIElement.PopupVersion.ADD:
                    items.ChangeItem(name, itemAmount + amountNum);
                    break;

                case PopupUIElement.PopupVersion.REMOVE:
                    if (amountNum >= itemAmount)
                    {
                        items.RemoveItem(name);
                        return;
                    }

                    items.ChangeItem(name, itemAmount - amountNum);
                    break;
                
                default:
                    break;
            }

        } else
        {
            if (version != PopupUIElement.PopupVersion.ADD) return;

            items.AddItem(name, amount);
        }


        @popupUI.SetEnabled(false);
    }
}
