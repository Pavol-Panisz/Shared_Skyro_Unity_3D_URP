using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MirkovaHra_InventorySystem : MonoBehaviour
{
    public static MirkovaHra_InventorySystem Instance;

    private Dictionary<MirkovaHra_ItemScriptableObject, int> _inventory = new Dictionary<MirkovaHra_ItemScriptableObject, int>();
    private Dictionary<MirkovaHra_ItemScriptableObject, MirkovaHra_ItemUIHandler> _itemUIs = new Dictionary<MirkovaHra_ItemScriptableObject, MirkovaHra_ItemUIHandler>();

    [SerializeField] private List<MirkovaHra_BuildResources> _startResources;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var resource in _startResources)
        {
            AddItems(resource.item, resource.count);
        }
    }

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
        UpdateUI();
    }

    public bool RemoveItems(Dictionary<MirkovaHra_ItemScriptableObject, int> items)
    {
        if(!AreThereItems(items))
            return false;

        foreach(var item in items)
        {
            _inventory[item.Key] -= item.Value;
            if (_inventory[item.Key] <= 0)
                _inventory.Remove(item.Key);
        }
        UpdateUI();
        return true;
    }

    public bool RemoveItems(MirkovaHra_ItemScriptableObject item, int count)
    {
        if(!AreThereItems(item, count))
            return false;

        _inventory[item] -= count;
        if (_inventory[item] <= 0)
            _inventory.Remove(item);
        UpdateUI();
        return true;
    }

    public bool AreThereItems(MirkovaHra_ItemScriptableObject item, int count)
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

    public bool AreThereItems(Dictionary<MirkovaHra_ItemScriptableObject, int> items)
    {
        foreach (var item in items)
        {
            if (!_inventory.ContainsKey(item.Key) || _inventory[item.Key] < item.Value)
                return false;
        }

        return true;
    }

    private void UpdateUI()
    {
        for(int i = _itemUIs.Count - 1; i >= 0; i--)
        {
            if (!_inventory.ContainsKey(_itemUIs.ElementAt(i).Key))
            {
                Destroy(_itemUIs.ElementAt(i).Value.gameObject);
                _itemUIs.Remove(_itemUIs.ElementAt(i).Key);
            }
        }

        foreach(var item in _inventory)
        {
            if(_itemUIs.ContainsKey(item.Key))
            {
                _itemUIs[item.Key].count = item.Value;
            }
            else
            {
                _itemUIs.Add(item.Key, CreateItemUI(item.Key, item.Value));
            }
        }
        /*for(int i = 0; i < MirkovaHra_GameUIHandler.Instance.inventoryTransform.childCount; i++)
        {
            Destroy(MirkovaHra_GameUIHandler.Instance.inventoryTransform.GetChild(i).gameObject);
        }

        foreach(var item in items)
        {

        }*/
    }

    private MirkovaHra_ItemUIHandler CreateItemUI(MirkovaHra_ItemScriptableObject item, int count)
    {
        MirkovaHra_ItemUIHandler uiHandler = Instantiate(Resources.Load<GameObject>("ItemUI")).GetComponent<MirkovaHra_ItemUIHandler>();
        uiHandler.transform.SetParent(MirkovaHra_GameUIHandler.Instance.inventoryContainer);
        //uiHandler.transform.localPosition = Vector3.zero;
        uiHandler.name = item.name;
        uiHandler.description = item.description;
        uiHandler.count = count;
        uiHandler.sprite = item.itemSprite;
        return uiHandler;
    }
}
