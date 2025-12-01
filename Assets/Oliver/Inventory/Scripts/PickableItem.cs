using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField]private ItemInstanceStruct itemInstance;
    [SerializeField]private bool randomAmount = true;

    private void Start() {
        if (randomAmount) itemInstance.count = Random.Range(1, itemInstance.itemData.maxCount);
    }

    private void PickUp()
    {
        ItemContainer.instance.AddItem(itemInstance);
        Destroy(gameObject);
    }

    private void OnMouseDown() {
        Debug.Log("Picked up item");
        PickUp();
    }
}
