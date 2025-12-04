using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryItem
{
    public ItemScriptableObject item;
    public int count;
}

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    private GameObject _itemUiPrefab;

    [SerializeField] private int _inventoryCapacity = 10;

    private Dictionary<int, InventoryItem> _beforeItems = new Dictionary<int, InventoryItem>();
    private Dictionary<int, InventoryItem> _items = new Dictionary<int, InventoryItem>();
    public Dictionary<int, InventoryItem> items { get { return _items; } }
    private List<InventoryIconUiHandler> _itemIcons = new List<InventoryIconUiHandler>();
    private int _curItemIndex;

    private Transform _inventorySelectedItemIndicator;
    [SerializeField] private Transform _itemsUIContainer;

    public delegate void OnInventoryChangedAction(Dictionary<int, InventoryItem> beforeInventory, Dictionary<int, InventoryItem> curInventory);
    public static event OnInventoryChangedAction onInventoryChanged;

    private void Awake()
    {
        Instance = this;

        _itemUiPrefab = Resources.Load<GameObject>("ItemUI_");
        print(_itemUiPrefab);
        _inventorySelectedItemIndicator = Instantiate(Resources.Load<GameObject>("SelectedItemIndicator")).transform;
        SetItemsUi(_items);
        SetCurItem(0);

        onInventoryChanged += OnInventoryChanged;
    }

    public bool AddItem(ItemScriptableObject item, int count)
    {
        int smallestIndex = 0;
        if (CanAddItem(out smallestIndex, item))
        {
            if(!_items.ContainsKey(smallestIndex))
            {
                _items.Add(smallestIndex, new InventoryItem { item = item, count = count });
            }
            else
            {
                _items[smallestIndex].count += count;
                OnInventoryChanged(null, _items);
            }

            if (onInventoryChanged != null) onInventoryChanged(_beforeItems, _items);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanAddItem(out int smallestIndex, ItemScriptableObject item)
    {
        for (int i = 0; i < _inventoryCapacity; i++)
        {
            if (!_items.ContainsKey(i))
            {
                smallestIndex = i;
                return true;
            }
            else
            {
                if (_items[i].item == item)
                {
                    smallestIndex = i;
                    return true;
                }
            }
        }
        smallestIndex = 0;
        return false;
    }

    public void OnNextItem(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        SetCurItem(GetPossibleItemIndex(_curItemIndex + (int)context.ReadValue<float>()));
    }

    private int GetPossibleItemIndex(int index)
    {
        index = index < 0 ? _inventoryCapacity - 1 : (index > _inventoryCapacity-1 ? 0 : index);
        return index;
    }

    private void SetCurItem(int itemIndex)
    {
        _curItemIndex = itemIndex;
        _inventorySelectedItemIndicator.SetParent(_itemIcons[itemIndex].transform);
        _inventorySelectedItemIndicator.localPosition = Vector3.zero;
    }

    private void OnInventoryChanged(Dictionary<int, InventoryItem> beforeInventory, Dictionary<int, InventoryItem> curInventory)
    {
        _beforeItems = curInventory;
        SetItemsUi(curInventory);
    }

    private void SetItemsUi(Dictionary<int, InventoryItem> items)
    {
        print("items list has been changed");
        for(int i = 0; i < _inventoryCapacity; i++)
        {
            if(_itemIcons.Count <= i)
            {
                InventoryIconUiHandler newHandler = Instantiate(_itemUiPrefab).GetComponent<InventoryIconUiHandler>();

                _itemIcons.Add(newHandler);
                newHandler.transform.parent = _itemsUIContainer;
            }

            InventoryIconUiHandler itemUi = _itemIcons[i];

            if (items.ContainsKey(i))
            {
                itemUi.name = items[i].item.name;
                itemUi.icon = items[i].item.sprite;
                itemUi.count = items[i].count;
            }
            else
            {
                itemUi.name = string.Empty;
                itemUi.icon = null;
                itemUi.count = 0;
            }
        }
    }
}
