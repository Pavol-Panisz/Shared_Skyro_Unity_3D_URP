using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ListsStorageFE : MonoBehaviour
{
    public RectTransform listItemsParent;
    public GameObject listItemPrefab;
    public List<TextMeshProUGUI> itemNamesTexts = new();
    public List<TextMeshProUGUI> itemCountsTexts = new();
    public string itemNameObjectTag;
    public string itemCountObjectTag;

    [Header("Data entry popup")]
    public TMP_InputField itemNameInputField;
    public TMP_InputField itemCountInputField;
    public TextMeshProUGUI promptTextField;
    public Button confirmButton;

    [Space]
    public UnityEvent OnOpenedDataEntry;
    public UnityEvent OnClosedDataEntry;

    [SerializeField] ListsStorage backend;

    private void Start()
    {
        backend = new ListsStorage();
    }

    // called when X is clicked
    public void CancelDataEntry()
    {
        OnClosedDataEntry.Invoke();
    }

    // called when Add or Remove is clicked
    public void OpenDataEntryWindow(string mode)
    {
        OnOpenedDataEntry.Invoke();
        confirmButton.onClick.RemoveAllListeners();
        itemNameInputField.Select();

        if (mode == "add")
        {
            promptTextField.text = "Add item";

            confirmButton.onClick.AddListener(() => {

                OnClosedDataEntry.Invoke();

                if (itemNameInputField.text.Trim() == "" || itemNameInputField.text == "")
                {
                    return;
                }

                string itemName = itemNameInputField.text;
                int itemCount = int.Parse(itemCountInputField.text);

                if (backend.Contains(itemName))
                {
                    backend.AddExistingItem(itemName, itemCount);

                    int index = backend.IndexOf(itemName);
                    int newCount = backend.CountOf(itemName);
                    itemCountsTexts[index].text = newCount.ToString();
                }
                else
                {
                    backend.AddNewItem(itemName, itemCount);
                    CreateNewListEntry(itemName, itemCount);
                }

            });
        }
        else if (mode == "remove")
        {
            promptTextField.text = "Remove item";

            confirmButton.onClick.AddListener(() => {

                OnClosedDataEntry.Invoke();

                if (itemNameInputField.text.Trim() == "" || itemNameInputField.text == "")
                {
                    return;
                }

                string itemName = itemNameInputField.text;
                int itemCount = int.Parse(itemCountInputField.text);

                if (backend.Contains(itemName))
                {

                }
                else
                {

                }

            });
        }
        else { Debug.LogError("Expected the mode string to be either add or remove"); }
    }

    // instantiates a new gameobject in the gui representing a single line in the list of items
    void CreateNewListEntry(string itemName, int itemCount)
    {
        var listItem = Instantiate(listItemPrefab, listItemsParent);

        foreach (Transform child in listItem.transform)
        {
            var tmpText = child.GetComponent<TextMeshProUGUI>();
            int newItemIndex = itemNamesTexts.Count;

            // add the child of the newly instantiated object either to
            // the list of item-name-text-gobjects or item-count-text-gobjects
            if (child.CompareTag(itemNameObjectTag))
            {
                itemNamesTexts.Add(tmpText);
                tmpText.text = itemName;
            }
            else if (child.CompareTag(itemCountObjectTag))
            {
                itemCountsTexts.Add(tmpText);
                tmpText.text = itemCount.ToString();
            }
            else { Debug.LogError("Error when working with tag of child"); }

        }
    }

}
