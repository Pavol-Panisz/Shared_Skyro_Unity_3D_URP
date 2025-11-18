using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.iOS;
using System.Text.RegularExpressions;

public class ItemHandler : MonoBehaviour
{
    public List<string> itemNames;
    public List<string> itemAmounts;

    public GameObject itemContainer;
    public GameObject itemText;

    public TextMeshProUGUI nameInput;
    public TextMeshProUGUI amountInput;

    private void Awake()
    {
    }

    public void AddItem()
    {
        TextMeshProUGUI itemObjectInput = itemText.GetComponent<TextMeshProUGUI>();


        string name = nameInput.text;
        string amount = amountInput.text;

        bool getName = itemNames.Contains(name);
        if (getName)
        {
            int index = itemNames.IndexOf(name);
            Debug.Log(index);
        }
        else if (!getName)
        {
            itemAmounts.Add(amount);
            itemNames.Add(name);

            TextMeshProUGUI itemAmountObject = Instantiate(itemObjectInput, itemContainer.transform);
            string itemValue = name + " - " + amount;
            itemAmountObject.text = itemValue;
        }






        Debug.Log(name);
        Debug.Log(amount);
    }
}
