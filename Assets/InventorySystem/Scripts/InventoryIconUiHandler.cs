using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryIconUiHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TMP_Text _itemsCount;

    private string _name;
    public string name {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            _itemName.text = value;
        }
    }

    private Sprite _icon;
    public Sprite icon
    {
        get
        {
            return _icon;
        }
        set
        {
            _icon = value;
            _itemIcon.sprite = value;
        }
    }

    private int _count;
    public int count {
        get
        {
            return _count;
        }
        set
        {
            _count = value;
            _itemsCount.text = value == 0 ? string.Empty : value.ToString();
        }
    }
}
