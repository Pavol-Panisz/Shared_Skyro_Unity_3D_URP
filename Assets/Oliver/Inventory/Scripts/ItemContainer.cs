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
        int amountToAdd = itemInstanceStruct.count;
        int index = 0;
        foreach (ItemInstanceClass itemInstance in items)
        {
            if (amountToAdd <= 0)
            {
                break;
            }

            if (itemInstance.itemData.itemName == itemInstanceStruct.itemData.itemName && itemInstance.count < itemInstanceStruct.itemData.maxCount)
            {
                itemInstance.AddCount(amountToAdd);
                amountToAdd = 0;

                if (itemInstance.count > itemInstanceStruct.itemData.maxCount)
                {
                    amountToAdd = itemInstance.count - itemInstanceStruct.itemData.maxCount;
                    itemInstance.SetCount(itemInstanceStruct.itemData.maxCount);
                }
            }

            index++;
        }

        if (amountToAdd > 0)
        {
            itemInstanceStruct.count = amountToAdd;
            items.Add(itemInstanceStruct);
        }

        FindAnyObjectByType<ItemContainerUI>().LoadUI();
    }
}
