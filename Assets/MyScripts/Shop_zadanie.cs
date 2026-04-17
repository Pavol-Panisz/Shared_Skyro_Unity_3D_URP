using TMPro;
using UnityEngine;

public class Shop_zadanie : MonoBehaviour
{
    public int money = 300;
    public int Price = 0;
    public int RemainingMoney = 0;

    public TMP_Text moneydisplay;
    public TMP_Text inventorydisplay;

    public int EvilIsac = 0;
    public int SharKhan = 0;
    public int JaneJuliet = 0;
    bool boughtItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {




    }

    public void Buy(string itemName)
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

            money = money - Price;
           
            {
                if (money < 0)
                {
                    Debug.Log("Broke Boy"); 
                    boughtItem = false;
                }
                else if (money > 0) 
                {
                    Debug.Log("Rich fella");
                    boughtItem= true;  
                
                }
                
               
                      
            }
            

            // Update is called once per frame



        
    }


    void Update()
    {
        moneydisplay.text = $"Money: {money}";
        inventorydisplay.text =
        $"MisterDich: {EvilIsac}, SharKhan: {SharKhan}, Larp: {JaneJuliet}";



    }
}

