using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor;
using System.Linq;

public class ArraysScript : MonoBehaviour
{
    public GameObject Text;
    public TMP_InputField AddNameInput;
    public TMP_InputField AddCountInput;
    public TMP_InputField RemoveNameInput;
    public TMP_InputField RemoveCountInput;

    public GameObject ItemNamesContent;
    public GameObject ItemCountContent;

    public List<string> ItemNames = new List<string>();
    public List<int> ItemCount = new List<int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem()
    {
        AddCountInput.text.ToLower();
        AddNameInput.text.ToLower();
        if (AddNameInput.text != "" && AddCountInput.text != "")
        {    
            int CountOfItems = int.Parse(AddCountInput.text);
            if (ItemNames.Count == 0)
            {
                GameObject createdNameText = Instantiate(Text, ItemNamesContent.transform);
                GameObject createdCountText = Instantiate(Text, ItemCountContent.transform);

                createdNameText.GetComponent<TMP_Text>().text = AddNameInput.text;
                createdCountText.GetComponent<TMP_Text>().text = CountOfItems.ToString();

                ItemNames.Add(createdNameText.GetComponent<TMP_Text>().text);
                ItemCount.Add(CountOfItems);
            }
            else if (!ItemNames.Contains(AddNameInput.text))
            {
                GameObject createdNameText = Instantiate(Text, ItemNamesContent.transform);
                GameObject createdCountText = Instantiate(Text, ItemCountContent.transform);

                createdNameText.GetComponent<TMP_Text>().text = AddNameInput.text;
                createdCountText.GetComponent<TMP_Text>().text = CountOfItems.ToString();

                ItemNames.Add(createdNameText.GetComponent<TMP_Text>().text);
                ItemCount.Add(CountOfItems);
            }
            else
            {
                ItemCount[ItemNames.IndexOf(AddNameInput.text)] += int.Parse(AddCountInput.text);
            }
        }
    }
    public void RemoveItem()
    {
        int CountOfItems = int.Parse(AddCountInput.text);
        for (int i = 0; i < ItemNames.Count; i++)
        {
            if (RemoveNameInput.Equals(ItemNames[i]) && int.Parse(RemoveCountInput.text) < int.Parse(ItemCount[i].ToString()))
            {
                ItemCount[i] -= CountOfItems;
            }
            else if(RemoveNameInput.Equals(ItemNames[i]) && int.Parse(RemoveCountInput.text) == int.Parse(ItemCount[i].ToString()))
            {
                ItemCount.RemoveAt(i);
                ItemNames.RemoveAt(i);
            }
            else
            {
                print("Error");
            }
        }
    }
}
