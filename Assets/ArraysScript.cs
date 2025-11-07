using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ArraysScript : MonoBehaviour
{
    public GameObject Text;
    public TMP_InputField AddNameInput;
    public TMP_InputField AddCountInput;
    public TMP_InputField RemoveNameInput;
    public TMP_InputField RemoveCountInput;

    public GameObject ItemNamesContent;
    public GameObject ItemCountContent;
    private List<Component> textComponent = new List<Component>();

    public List<string> ItemNames = new List<string>();
    public List<int> ItemCount = new List<int>();

    private void Awake()
    {
        
    }
    public void AddItem()
    {

        if (AddNameInput.text != "" && AddCountInput.text != "")
        {
            string CurrentAddNameInput = AddNameInput.text.ToLowerInvariant();
            string CurrentCountInput = AddCountInput.text.ToLowerInvariant();

            if (ItemNames.Count == 0)
            {
                GameObject createdNameText = Instantiate(Text, ItemNamesContent.transform);
                GameObject createdCountText = Instantiate(Text, ItemCountContent.transform);

                createdNameText.GetComponent<TMP_Text>().text = CurrentAddNameInput;
                createdCountText.GetComponent<TMP_Text>().text = CurrentCountInput;

                ItemNames.Add(createdNameText.GetComponent<TMP_Text>().text);
                ItemCount.Add(int.Parse(CurrentCountInput));
                textComponent.Add(createdCountText.GetComponent<TMP_Text>());
            }
            else if (!ItemNames.Contains(CurrentAddNameInput))
            {
                GameObject createdNameText = Instantiate(Text, ItemNamesContent.transform);
                GameObject createdCountText = Instantiate(Text, ItemCountContent.transform);

                createdNameText.GetComponent<TMP_Text>().text = CurrentAddNameInput;
                createdCountText.GetComponent<TMP_Text>().text = CurrentCountInput;

                ItemNames.Add(createdNameText.GetComponent<TMP_Text>().text);
                ItemCount.Add(int.Parse(CurrentCountInput));
                textComponent.Add(createdCountText.GetComponent<TMP_Text>());
            }
            else
            {
                ItemCount[ItemNames.IndexOf(CurrentAddNameInput)] += int.Parse(CurrentCountInput);
                textComponent[ItemNames.IndexOf(CurrentAddNameInput)].GetComponent<TMP_Text>().text = ItemCount[ItemNames.IndexOf(CurrentAddNameInput)].ToString();
            }
        }
    }
    public void RemoveItem()
    {
        string CurrentRemoveNameInput = RemoveNameInput.text.ToLowerInvariant();
        string CurrentRemoveInput = RemoveCountInput.text.ToLowerInvariant();

            if (CurrentRemoveNameInput.Equals(ItemNames[ItemNames.IndexOf(CurrentRemoveNameInput)]) && int.Parse(CurrentRemoveInput) < ItemCount[ItemNames.IndexOf(CurrentRemoveNameInput)])
            {
                ItemCount[ItemNames.IndexOf(CurrentRemoveNameInput)] -= int.Parse(CurrentRemoveInput);
                textComponent[ItemNames.IndexOf(CurrentRemoveNameInput)].GetComponent<TMP_Text>().text = ItemCount[ItemNames.IndexOf(CurrentRemoveNameInput)].ToString();

            }
            else if(CurrentRemoveNameInput.Equals(ItemNames[ItemNames.IndexOf(CurrentRemoveNameInput)]) && int.Parse(CurrentRemoveInput) == ItemCount[ItemNames.IndexOf(CurrentRemoveNameInput)])
            {
                ItemCount.RemoveAt(ItemNames.IndexOf(CurrentRemoveNameInput));
                ItemNames.RemoveAt(ItemNames.IndexOf(CurrentRemoveNameInput));
            }
            else
            {
                Destroy(ItemNamesContent.GetComponentInChildren<TMP_Text>());
                ItemCount.RemoveAt(ItemNames.IndexOf(CurrentRemoveNameInput));
                ItemNames.RemoveAt(ItemNames.IndexOf(CurrentRemoveNameInput));
                print("Error");
            }
        }
}
