using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private Transform contentObj;
    [SerializeField] private GameObject itemPanelPrefab;

    public void AddItem(string @name, string @value)
    {
        if (contentObj.Find(@name)) return;
        GameObject itemPanel = Instantiate(itemPanelPrefab);
        itemPanel.transform.parent = contentObj;
        ItemPanelUIElement uIElement = itemPanel.GetComponent<ItemPanelUIElement>();
        uIElement.SetName(@name);
        uIElement.SetValue(@value);
        itemPanel.name = @name;
    }

    public void ChangeItem(string @name, int @newValue)
    {
        Transform t = contentObj.Find(@name);
        if (!t) return;
        ItemPanelUIElement uIElement = t.GetComponent<ItemPanelUIElement>();
        uIElement.SetValue(@newValue.ToString());
    }
    
    public void RemoveItem(string @name)
    {
        Transform t = contentObj.Find(@name);
        if (!t) return;
        Destroy(t.gameObject);
    }
}
