using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField]private List<ItemInstanceClass> items;

    public static ItemContainer instance;

    private void Awake() {
        if (instance)
        {
            Debug.LogWarning("Instance already exists!!!");
            Destroy(this);        
        }
        else
        {
            instance = this;
        }
    }

    public List<ItemInstanceClass> GetItems()
    {
        return items;
    }

    public void AddItem(ItemInstanceClass itemInstanceStruct)
    {
        //items.Add(itemInstanceStruct);

        int amountToAdd = itemInstanceStruct.count;
        int index = 0;
        foreach (ItemInstanceClass itemInstance in items)
        {
            if (amountToAdd <= 0)
            {
                Debug.Log("break");
                break;
            }

            Debug.Log(itemInstance.itemData.itemName + " " + itemInstanceStruct.itemData.itemName);
            Debug.Log(itemInstance.itemData.itemName == itemInstanceStruct.itemData.itemName);

            if (itemInstance.itemData.itemName == itemInstanceStruct.itemData.itemName && itemInstance.count < itemInstanceStruct.itemData.maxCount)
            {
                itemInstance.AddCount(amountToAdd);
                Debug.Log("add");
                amountToAdd = 0;

                if (itemInstance.count > itemInstanceStruct.itemData.maxCount)
                {
                    amountToAdd = itemInstance.count - itemInstanceStruct.itemData.maxCount;
                    itemInstance.SetCount(itemInstanceStruct.itemData.maxCount);
                    Debug.Log("Set");
                }
            }

            index++;
        }

        if (amountToAdd > 0)
        {
            itemInstanceStruct.count = amountToAdd;
            items.Add(itemInstanceStruct);
            Debug.Log("Spawn");
        }

        FindAnyObjectByType<ItemContainerUI>().LoadUI();
    }
}
