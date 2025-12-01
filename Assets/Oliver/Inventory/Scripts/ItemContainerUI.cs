using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemContainerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private ItemContainer itemContainer;

    [Header("UI")]
    [SerializeField]private GameObject itemUIPrefab;
    [SerializeField]private Transform content;

    [SerializeField]private string countPrefix = "Count: ";

    private List<GameObject> spawnedUIItemList = new List<GameObject>();

    private void Start() {
        LoadUI();
    }

    public void LoadUI()
    {
        for (int i = 0; i < spawnedUIItemList.Count; i++)
        {
            Destroy(spawnedUIItemList[i]);
        }

        spawnedUIItemList.Clear();

        foreach (ItemInstanceStruct itemInstance in itemContainer.GetItems())
        {
            GameObject spawnedUIItem = Instantiate(itemUIPrefab, content);

            //Set Texts
            spawnedUIItem.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = itemInstance.itemData.itemName;
            spawnedUIItem.transform.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = itemInstance.itemData.desc;
            spawnedUIItem.transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>().text = countPrefix + itemInstance.count.ToString();

            spawnedUIItemList.Add(spawnedUIItem);        
        }
    }
}
