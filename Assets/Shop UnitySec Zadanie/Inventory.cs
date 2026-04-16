using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Item counts")]
    public int swords = 0;
    public int bows = 0;
    public int arrows = 0;

    public void AddItem(string itemName)
    {
        if (itemName == "sword")
        {
            swords += 1;
        }
        else if (itemName == "bow")
        {
            bows += 1;
        }
        else if (itemName == "arrow")
        {
            arrows += 1;
        }
    }
}
