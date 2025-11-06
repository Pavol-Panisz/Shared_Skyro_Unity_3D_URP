using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public class ArraysScript : MonoBehaviour
{
    public GameObject Text;
    public TMP_InputField AddNameInput;
    public TMP_InputField AddCountInput;
    public TMP_InputField RemoveNameInput;
    public TMP_InputField RemoveCountInput;

    public GameObject ItemNamesContent;
    public GameObject ItemCountContent;

    public List<string> ItemNames;
    public List<int> ItemCount;
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
        if (AddNameInput.text != "" && AddCountInput.text != "")
        {    
            int CountOfItems = int.Parse(AddCountInput.text);
            for (int i = 0; i < ItemNames.Count; i++)
            {
                if (AddNameInput.Equals(ItemNames[i]))
                {
                    ItemCount[i] += CountOfItems;
                }
                else
                {
                    GameObject createdNameText = Instantiate(Text, ItemNamesContent.transform);
                    GameObject createdCountText = Instantiate(Text, ItemCountContent.transform);

                    createdNameText.GetComponent<TMP_Text>().text = AddNameInput.text;
                    createdCountText.GetComponent<TMP_Text>().text = CountOfItems.ToString();

                    ItemNames.Add(createdNameText.GetComponent<TMP_Text>().text);
                    ItemCount.Add(CountOfItems);
                }
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
