using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemScriptableObject _item;
    [SerializeField] private int _count = 1;

    private void OnTriggerEnter(Collider other)
    {
        InventorySystem inventorySystem = other.GetComponent<InventorySystem>();

        if(inventorySystem)
        {
            if(inventorySystem.AddItem(_item, _count))
            {
                Destroy(gameObject);
            }
        }
    }
}
