//using UnityEngine;
//using System.Collections.Generic;
//using TMPro;
//using System;
//using UnityEngine.TestTools;

//public class ArraysScript : MonoBehaviour
//{
//    public GameObject Text;
//    public TMP_InputField AddNameInput;
//    public TMP_InputField AddCountInput;
//    public TMP_InputField RemoveNameInput;
//    public TMP_InputField RemoveCountInput;

//    public GameObject ItemNamesContent;
//    public GameObject ItemCountContent;

//    [Serializable]
//    public class Items
//    {
//        public string ItemNames;
//        public string ItemCount;
//        public Component textComponent;
//        public GameObject nameTextObject;
//        public GameObject countTextObject;
//    }

//    public List<Items> itemsList;
//    public void AddItem()
//    {
//        Items myItems = new Items();
//        if (AddNameInput.text != "" && AddCountInput.text != "")
//        {
//            string CurrentAddNameInput = AddNameInput.text.ToLowerInvariant();
//            string CurrentCountInput = AddCountInput.text.ToLowerInvariant();
//            int Index = itemsList.FindIndex(x => x.ItemNames == CurrentAddNameInput);
//            int itemcount = int.Parse(itemsList[Index].ItemCount);

//            if (itemsList.Count == 0)
//            {
//                GameObject createdNameText = Instantiate(Text, ItemNamesContent.transform);
//                GameObject createdCountText = Instantiate(Text, ItemCountContent.transform);

//                createdNameText.GetComponent<TMP_Text>().text = CurrentAddNameInput;
//                createdCountText.GetComponent<TMP_Text>().text = CurrentCountInput;

//                itemsList.Add(myItems.ItemNames = CurrentAddNameInput);
//                itemsList.Add(myItems.ItemCount = CurrentCountInput);
//                itemsList.Add(myItems.textComponent = createdNameText.GetComponent<TMP_Text>());
//                itemsList.Add(myItems.nameTextObject = createdNameText);
//                itemsList.Add(myItems.countTextObject = createdCountText);
//            }
//            else if (!myItems.ItemNames.Contains(CurrentAddNameInput))
//            {
//                GameObject createdNameText = Instantiate(Text, ItemNamesContent.transform);
//                GameObject createdCountText = Instantiate(Text, ItemCountContent.transform);

//                createdNameText.GetComponent<TMP_Text>().text = CurrentAddNameInput;
//                createdCountText.GetComponent<TMP_Text>().text = CurrentCountInput;

//                ItemNames.Add(createdNameText.GetComponent<TMP_Text>().text);
//                ItemCount.Add(int.Parse(CurrentCountInput));
//                textComponent.Add(createdCountText.GetComponent<TMP_Text>());
//                nameTextObjects.Add(createdNameText);
//                countTextObjects.Add(createdCountText);
//            }
//            else
//            {
//                itemcount += int.Parse(CurrentCountInput);
//                itemsList[Index].textComponent.GetComponent<TMP_Text>().text = itemsList[Index].ItemCount.ToString();
//            }
//        }
//    }
//    public void RemoveItem()
//    {
//        string CurrentRemoveNameInput = RemoveNameInput.text.ToLowerInvariant();
//        string CurrentRemoveInput = RemoveCountInput.text.ToLowerInvariant();
//        int Index = itemsList.FindIndex(x => x.ItemNames == CurrentRemoveNameInput);
//        int itemcount = int.Parse(itemsList[Index].ItemCount);

//        if (CurrentRemoveNameInput.Equals(itemsList[Index].ItemNames) && int.Parse(CurrentRemoveInput) < int.Parse(itemsList[Index].ItemCount))
//        {
//            itemcount -= int.Parse(CurrentRemoveInput);
//            itemsList[Index].textComponent.GetComponent<TMP_Text>().text = itemsList[Index].ItemCount.ToString();

//        }
//        else if (CurrentRemoveNameInput.Equals(itemsList[Index].ItemNames) && CurrentRemoveInput == itemsList[Index].ItemCount)
//        {
//            Destroy(itemsList[Index].nameTextObject);
//            Destroy(itemsList[Index].countTextObject);
//            itemsList[Index].ItemNames = string.Empty;
//            itemsList[Index].ItemCount = string.Empty;
//        }
//        else
//        {
//            Destroy(itemsList[Index].nameTextObject);
//            Destroy(itemsList[Index].countTextObject);
//            itemsList[Index].ItemNames = string.Empty;
//            itemsList[Index].ItemCount = string.Empty;
//            print("Error");
//        }
//    }
//}