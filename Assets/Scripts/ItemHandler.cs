using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ItemHandler : MonoBehaviour
{
    public List<string> itemNames;
    public TextMeshProUGUI nameInput;

    private void Awake()
    {

    }
    public void AddItem()
    {
        string name = nameInput.text;
        itemNames.Add(name);
        Debug.Log("coc");
    }
}
