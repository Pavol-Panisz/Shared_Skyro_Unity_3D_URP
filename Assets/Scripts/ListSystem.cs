using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ItemHandlerNew : MonoBehaviour
{
    public TextMeshProUGUI nameInput; 
    public TextMeshProUGUI amountInput;

    public List<string> itemNames;
    public List<int> itemAmount;



    public void AddItem()
    {
        string name = nameInput.text;
        int amount = int.Parse(amountInput.text);

        if (amount != 0)
        {
            if (itemNames.Contains(name)) 
            {
                int index = itemNames.IndexOf(name);
                int ogAmount = itemAmount[index] + amount; 
                print(itemAmount[index]);
            }
            if (!itemNames.Contains(name))
            {
                itemNames.Add(name);
                itemAmount.Add(amount);
            }
        }
            
    }
}
