using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ListsStorage
{
    public class Item
    {
        public string name;
        public int count;

        public Item(string name, int count)
        {
            this.name = name;
            this.count = count;
        }

    }
    List<Item> itemsList;

    //List<string> itemNames = new();
    //List<int> itemCounts = new();

    public bool Contains(string name)
    {
        
        for (int i = 0; i < itemsList.Count; i++) 
        {
            if (itemsList[i].name == name)
            {
                return true;
            }
        }
        return false;
    }

    public int IndexOf(string name) => itemsList.FindIndex(x => x.name == name);

    public int CountOf(string name) => itemsList[IndexOf(name)].count;

    public void AddExistingItem(string itemName, int itemCount)
    { 
        int indexOfItem = itemsList.FindIndex(x => x.name == itemName);
        itemsList[indexOfItem].count += itemCount;
    }

    public void AddNewItem(string itemName, int itemCount)
    {
        Item newItemToAdd = new Item(itemName, itemCount);

        itemsList.Add(newItemToAdd);
    }

    public void RemoveExistingItem(string itemName)
    { 
        int index = IndexOf(itemName);
        //itemNames.RemoveAt(index);
    }

}
