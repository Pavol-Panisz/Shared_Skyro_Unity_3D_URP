using UnityEngine;

[CreateAssetMenu(fileName = "newItemScriptableObject", menuName = "InventorySystem/Create new Item Scriptable Object")]
public class ItemScriptableObject : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public GameObject droppedItemPrefab;
}
