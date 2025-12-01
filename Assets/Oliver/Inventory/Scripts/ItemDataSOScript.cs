using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSOScript", menuName = "Scriptable Objects/ItemDataSOScript")]
public class ItemDataSOScript : ScriptableObject
{
    public string itemName;
    [TextArea] public string desc;
    public Sprite image;
    public GameObject itemPrefab;
    public int maxCount;
}
