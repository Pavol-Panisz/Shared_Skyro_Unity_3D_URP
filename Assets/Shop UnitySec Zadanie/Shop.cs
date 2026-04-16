using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public int money = 100;

    // ktory item mas selectnuty v inventari.
    // moze byt "sword" alebo "bow"
    public string selectedItem = "none";

    [Header("Item counts")]
    public int swords = 0;
    public int bows = 0;
    public int arrows = 0;

    [Header("Item prices")]
    public int swordPrice = 10;
    public int bowPrice = 20;
    public int arrowPrice = 2;


    [Header("References to other stuff")]
    public TextMeshProUGUI inventoryText;
    public TextMeshProUGUI moneyText;
    public GameObject shopUI;
    public GameObject sword;
    public GameObject bow;

    [Header("Events")]
    public UnityEvent OnOpenedShop;
    public UnityEvent OnClosedShop;
    
    public void Buy(string item)
    {
        if (item == "Sword")
        {
            if (money >= swordPrice)
            {
                money -= swordPrice;
                swords++;
            }
        }

        if (item == "Bow")
        {
            if (money >= bowPrice)
            {
                money -= bowPrice;
                bows++;
            }
        }

        if (item == "Arrow")
        {
            if (money >= arrowPrice)
            {
                money -= arrowPrice;
                arrows++;
            }
        }
    }

    void Update()
    {
        inventoryText.text = 
            $"sword: {swords}, bows: {bows} arrows: {arrows}";
        moneyText.text = $"money: {money}";

        // ked stlacis 1 a vlastnis mec, zapni mec
        if (Input.GetKeyDown(KeyCode.Keypad1) && swords > 0)
        {
            selectedItem = "sword";
            sword.SetActive(true);
            bow.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && bows > 0)
        {
            selectedItem = "bow";
            sword.SetActive(false);
            bow.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shopUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            OnOpenedShop.Invoke();
        }
    }

    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        shopUI.SetActive(false);
        OnClosedShop.Invoke();
    }
}