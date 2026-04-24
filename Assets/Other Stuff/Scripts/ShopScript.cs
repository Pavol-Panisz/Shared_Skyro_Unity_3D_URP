using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopWithInventory : MonoBehaviour
{
    public int money = 200;
    public TextMeshProUGUI moneyDisplay;
    public TextMeshProUGUI inventoryDisplay;
    public TextMeshProUGUI notEnoughCoinsText;

    private Coroutine notEnoughCoinsCoroutine;
    private Coroutine closeShopCoroutine;

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

    void Start()
    {
        if (notEnoughCoinsText != null)
        {
            notEnoughCoinsText.gameObject.SetActive(false);
        }
    }

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

    public void PressedBuy()
    {
        GameObject clickedButton = EventSystem.current != null
            ? EventSystem.current.currentSelectedGameObject
            : null;

        if (clickedButton == null)
        {
            Debug.LogWarning("PressedBuy: could not detect clicked button.");
            return;
        }

        TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
        if (buttonText == null)
        {
            Debug.LogWarning("PressedBuy: clicked button has no TMP text.");
            return;
        }

        if (!TryGetItemAndPriceFromButtonText(buttonText.text, out string itemName, out int parsedPrice))
        {
            Debug.LogWarning($"PressedBuy: invalid button text format '{buttonText.text}'. Use 'Item - Price'.");
            return;
        }

        priceToPay = parsedPrice;
        
        // here i am seeing whether i have enough money,
        // and if yes, i actually buy the thing
        moneyAfterPurchase = money - priceToPay;

        if (moneyAfterPurchase < 0)
        {
            Debug.Log("Not enough money!");
            hasBoughtItem = false;

            if (notEnoughCoinsText != null)
            {
                if (notEnoughCoinsCoroutine != null)
                {
                    StopCoroutine(notEnoughCoinsCoroutine);
                }

                notEnoughCoinsCoroutine = StartCoroutine(ShowNotEnoughCoinsForDelay(5f));
            }
        }
        else
        {
            money -= priceToPay;
            hasBoughtItem = true;
        }

        // now that we have potentially bought the item (by which
        // I mean that we subtracted our money, it's 
        // time to potentially update the inventory


        if (hasBoughtItem == true)
        {
            string normalizedItemName = itemName.Trim().ToLowerInvariant();

            if (normalizedItemName == "sword")
            {
                swords += 1;
            }
            if (normalizedItemName == "bow")
            {
                bows += 1;
            }
            if (normalizedItemName == "arrow")
            {
                arrows += 1;
            }
        }

        if (closeShopCoroutine != null)
        {
            StopCoroutine(closeShopCoroutine);
        }

        closeShopCoroutine = StartCoroutine(CloseShopAfterDelay(5f));
    }

    bool TryGetItemAndPriceFromButtonText(string buttonLabel, out string itemName, out int itemPrice)
    {
        itemName = "";
        itemPrice = 0;

        if (string.IsNullOrWhiteSpace(buttonLabel))
        {
            return false;
        }

        int separatorIndex = buttonLabel.LastIndexOf('-');
        if (separatorIndex <= 0 || separatorIndex >= buttonLabel.Length - 1)
        {
            return false;
        }

        itemName = buttonLabel.Substring(0, separatorIndex).Trim();
        string pricePart = buttonLabel.Substring(separatorIndex + 1).Trim();

        string digitsOnly = "";
        foreach (char character in pricePart)
        {
            if (char.IsDigit(character))
            {
                digitsOnly += character;
            }
        }

        if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(digitsOnly))
        {
            return false;
        }

        return int.TryParse(digitsOnly, out itemPrice);
    }

    System.Collections.IEnumerator CloseShopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shopCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        closeShopCoroutine = null;
    }

    System.Collections.IEnumerator ShowNotEnoughCoinsForDelay(float delay)
    {
        notEnoughCoinsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        notEnoughCoinsText.gameObject.SetActive(false);
        notEnoughCoinsCoroutine = null;
    }
}
