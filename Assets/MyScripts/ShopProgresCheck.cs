using NUnit.Framework;
using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int money = 300;
    public TextMeshProUGUI moneydisplay;
    public TextMeshProUGUI inventorydisplay;

    public int EvilIsac = 0;
    public int SharKhan = 0;
    public int JaneJuliet = 0;

    public int Price = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void PressedBuy(string itemName)
    {
        if (itemName == "EvilIsac")
        {
            Price = 27;
        }
        if (itemName == "SharKhan")
        {
            Price = 100;
        }
        if (itemName == "JaneJuliet")
        {
            Price = 67;
        }

       
        // Update is called once per frame


        void Update()
        {
            moneydisplay.text = $"Money: {money}";
            inventorydisplay.text =
            $"Evil Isac: {EvilIsac}, Sharkhnan: {SharKhan}, Larp: {JaneJuliet}";
        }
    }
}



