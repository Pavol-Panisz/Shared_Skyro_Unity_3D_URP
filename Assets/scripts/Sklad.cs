using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization;
using System.Diagnostics;

public class Sklad : MonoBehaviour
{

    [Header("Item names list")]
    public List<string> userInputList = new List<string>();
    string userInputText;
    bool isItemIncluded;
    [SerializeField] TMP_InputField IFnameOfItem;
    [SerializeField] TMP_InputField IFcoutOfItem;


    [Header("Cout of items list")]
    public List<int> coutOfItemsList = new List<int>();
    int lastAddedItemIndex;
    int indexOfIncludedItem;
    int howManyItems;

    public void ClickedAdd()
    {
        howManyItems = int.Parse(IFcoutOfItem.text);
        if (userInputList.Contains(userInputText) == false)
        {
            userInputList.Add(userInputText);
            coutOfItemsList.Add(howManyItems);
        }
        else if (userInputList.Contains(userInputText) == true)
        {
            indexOfIncludedItem = userInputList.IndexOf(userInputText);
            coutOfItemsList[indexOfIncludedItem] += howManyItems;
        }
    }

    public void ClickedRemove()
    {
        if(userInputList.Contains(userInputText) == true)
        {
            howManyItems = int.Parse(IFcoutOfItem.text);
            int whatToRemove = userInputList.IndexOf(userInputText);
            if (coutOfItemsList[whatToRemove] == 1)
            {
                userInputList.RemoveAt(whatToRemove);
                coutOfItemsList.RemoveAt(whatToRemove);
            }
            else if (coutOfItemsList[whatToRemove] > 1)
            {
                coutOfItemsList[whatToRemove] -= howManyItems;
            }
            
        }
    }

    public void EditedTextName()
    {
        userInputText = IFnameOfItem.text.ToString();
    }
    
    public void EditedTextCount()
    {
        
    }
}
