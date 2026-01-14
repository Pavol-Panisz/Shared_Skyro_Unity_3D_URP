using System;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    [Serializable]
    public class Items
    {
        public string name;
        public int count;
        public float rarity;
    }

    public List<Items> itemList;

    public void Start()
    {
        Items FirstItem = new Items();

        FirstItem.name = "head";
        FirstItem.count = 5;
        FirstItem.rarity = 15.25f;

        itemList.Add(FirstItem);
    }
}
