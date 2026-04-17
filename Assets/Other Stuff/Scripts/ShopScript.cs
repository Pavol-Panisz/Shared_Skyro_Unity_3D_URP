using UnityEngine;
using TMPro;

public class ShopWithInventory : MonoBehaviour
{
    public int money = 200;
    public TextMeshProUGUI moneyDisplay;
    public TextMeshProUGUI inventoryDisplay;

    // these variables are used for calculating
    // whether I have enough monet to buy the item:

    // stores how much money would I have after the purchase
    int moneyAfterPurchase = 0; 
    // stores how much the item will cost
    int priceToPay = 0;         
    // stores whether the item was actually bought 
    // (so we had enough money for the purchase)
    bool hasBoughtItem = false; 

    // inventory variables:

    public GameObject shopCanvas;

    public int swords = 0;
    public int bows = 0;
    public int arrows = 0;

    void Update()
    {
        moneyDisplay.text = $"Money: {money}";

        inventoryDisplay.text = 
            $"Sword: {swords}, Bows: {bows}, Arrows: {arrows}";
    }

    public void OpenShop()
    {
        shopCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PressedBuy(string itemName)
    {
        // here i am just determinig how much to pay
        if (itemName == "Sword")
        {
            priceToPay = 50;
        }
        if (itemName == "Bow")
        {
            priceToPay = 80;
        }
        if (itemName == "Arrow")
        {
            priceToPay = 5;
        }
        
        // here i am seeing whether i have enough money,
        // and if yes, i actually buy the thing
        moneyAfterPurchase = money - priceToPay;

        if (moneyAfterPurchase < 0)
        {
            Debug.Log("Not enough money!");
            hasBoughtItem = false;
        }
        else
        {
            money -= priceToPay;
            hasBoughtItem = true;
        }

        // now that we have potentially bought the item (by which
        // I mean that we subtracted our money, it's 
        // time to potentially update the inventory


        if (itemName == "Sword" && hasBoughtItem == true)
        {
            swords += 1;
        }
        if (itemName == "Bow" && hasBoughtItem == true)
        {
            bows += 1;
        }
        if (itemName == "Arrow" && hasBoughtItem == true)
        {
            arrows += 1;
        }

        StartCoroutine(CloseShopAfterDelay(5f));
    }

    System.Collections.IEnumerator CloseShopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shopCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
