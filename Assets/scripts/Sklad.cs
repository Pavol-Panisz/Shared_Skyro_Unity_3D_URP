using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sklad : MonoBehaviour
{

    public List<string> userInputList = new List<string>();
    string userInputText;
    [SerializeField] TMP_InputField IFnameOfItem;
    [SerializeField] TMP_InputField IFcoutOfItem;
    [SerializeField] TextMeshProUGUI text; 
    [SerializeField] TextMeshProUGUI listText;
    [SerializeField] TextMeshProUGUI countText;


    public List<int> coutOfItemsList = new List<int>();
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
            howManyItems = int.Parse(IFcoutOfItem.text);
            userInputList.Add(userInputText);
            coutOfItemsList.Add(howManyItems);
            SetText();
            
        }
        else if (userInputList.Contains(userInputText) == true && isInputCorect == true)
        {
            howManyItems = int.Parse(IFcoutOfItem.text);
            indexOfIncludedItem = userInputList.IndexOf(userInputText);
            coutOfItemsList[indexOfIncludedItem] += howManyItems;
            SetText();
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
                SetText();
            }
            else if (coutOfItemsList[whatToRemove] >= 1)
            {
                coutOfItemsList[whatToRemove] -= howManyItems;
                SetText();
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

    private void SetText()
    {
        listText.text = "";
        countText.text = "";
        foreach (string items in userInputList)
        {
            listText.text += items + '\n';
        }
        foreach (int countOfItems in coutOfItemsList)
        {
            countText.text += countOfItems + '\n'.ToString();
        }
    }
}
