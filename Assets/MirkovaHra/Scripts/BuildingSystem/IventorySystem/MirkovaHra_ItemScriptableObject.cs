using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Mirkova Hra/Inventory System/Create new Item")]
public class MirkovaHra_ItemScriptableObject : ScriptableObject
{
    public string name;
    public string description;
    public Sprite itemSprite;
}
