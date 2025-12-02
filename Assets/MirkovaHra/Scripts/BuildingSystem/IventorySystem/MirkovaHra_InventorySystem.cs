using System.Collections.Generic;
using UnityEngine;

public class MirkovaHra_InventorySystem : MonoBehaviour
{
    private Dictionary<MirkovaHra_ItemScriptableObject, int> _inventory;

    public void AddItems(MirkovaHra_ItemScriptableObject item, int count)
    {
        if(_inventory.ContainsKey(item))
        {
            _inventory[item] += count;
        }
        else
        {
            _inventory.Add(item, count);
        }
    }

    public bool RemoveItem(MirkovaHra_ItemScriptableObject item, int count)
    {
        if(!AreThereItems(item, count))
            return false;

        _inventory[item] -= count;
        if (_inventory[item] <= 0)
            _inventory.Remove(item);
        return true;
    }

    private bool AreThereItems(MirkovaHra_ItemScriptableObject item, int count)
    {
        if (_inventory.ContainsKey(item))
        {
            if (_inventory[item] >= count)
            {
                return true;
            }
        }
        return false;
    }
}
