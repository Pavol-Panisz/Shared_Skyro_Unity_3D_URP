using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassSystem : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField countInput;
    private string nameFromInput;
    private int countFromInput;
    [Serializable] public class Item
    {
        public string name;
        public int count;

        public Item(string newName, int newCount)
        {
            name = newName;
            count = newCount;
        }
    }

    public List<Item> itemsList;
    
    public void AddItem()
    {
        for(int i = 0; i < itemsList.Count; i++)
        {
            if (itemsList[i].name != name)
            {
                itemsList[i].count + ;
                break;
            }
        }

        Item firstItem = new Item(nameInput.text, int.Parse(countInput.text));

        itemsList.Add(firstItem);
    }


}
