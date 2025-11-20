using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization;
using System.Dynamic;
using System.Linq;

public class Sklad : MonoBehaviour
{

    public List<string> userInputList = new List<string>();
    string userInputText;
    bool isItemIncluded;
    [SerializeField] TMP_InputField IFnameOfItem;
    [SerializeField] TMP_InputField IFcoutOfItem;
    [SerializeField] TextMeshProUGUI text; 
    [SerializeField] TextMeshProUGUI listText;
    [SerializeField] TextMeshProUGUI countText;


    public List<int> coutOfItemsList = new List<int>();
    int lastAddedItemIndex;
    int indexOfIncludedItem;
    int howManyItems;
    bool isInputCorect;

    public void ClickedAdd()
    {
        if (string.IsNullOrEmpty(IFcoutOfItem.text) == true || int.Parse(IFcoutOfItem.text) < 0)
        {
            text.text = "You entered en invalid count of items";
            Invoke(nameof(HideText), 3f);
            isInputCorect = false;
        }
        else
        {
            isInputCorect = true;
        }
        
        if (userInputList.Contains(userInputText) == false && isInputCorect == true)
        {
            userInputList.Add(userInputText);
            coutOfItemsList.Add(howManyItems);
        }
        else if (userInputList.Contains(userInputText) == true && isInputCorect == true)
        {
            indexOfIncludedItem = userInputList.IndexOf(userInputText);
            coutOfItemsList[indexOfIncludedItem] += howManyItems;
        }
    }

    public void ClickedRemove()
    {
        if (userInputList.Contains(userInputText) == true)
        {
            howManyItems = int.Parse(IFcoutOfItem.text);
            int whatToRemove = userInputList.IndexOf(userInputText);
            if (coutOfItemsList[whatToRemove] == 1 || coutOfItemsList[whatToRemove] == howManyItems || coutOfItemsList[whatToRemove] <= howManyItems)
            {
                userInputList.RemoveAt(whatToRemove);
                coutOfItemsList.RemoveAt(whatToRemove);
            }
            else if (coutOfItemsList[whatToRemove] >= 1)
            {
                coutOfItemsList[whatToRemove] -= howManyItems;
            }

        }
        else
        {
            text.text = "You are trying to remove invalid item";
            Invoke(nameof(HideText), 3f);
        }
    }

    public void EditedTextName()
    {
        userInputText = IFnameOfItem.text.ToString();
    }

    private void HideText()
    {
        text.text = "";
    }
}
