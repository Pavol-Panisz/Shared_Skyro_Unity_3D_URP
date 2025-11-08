using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ListsStorage
{
    List<string> itemNames = new();
    List<int> itemCounts = new();

    public bool Contains(string name) => itemNames.Contains(name);

    public int IndexOf(string name) => itemNames.IndexOf(name);

    public int CountOf(string name) => itemCounts[IndexOf(name)];

    public void AddExistingItem(string itemName, int itemCount)
    {
        int index = itemNames.IndexOf(itemName);
        int newCount = itemCounts[index] + itemCount;
        itemCounts[index] = newCount;
    }

    public void AddNewItem(string itemName, int itemCount)
    {
        itemNames.Add(itemName);
        itemCounts.Add(itemCount);
    }

    public void RemoveExistingItem(string itemName)
    { 
        int index = IndexOf(itemName);
        itemNames.RemoveAt(index);
    }

}
