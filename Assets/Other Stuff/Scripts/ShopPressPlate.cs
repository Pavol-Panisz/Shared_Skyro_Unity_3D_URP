using UnityEngine;

public class ShopPressPlate : MonoBehaviour
{
    [Tooltip("Referencia na ShopWithInventory skript")]
    public ShopWithInventory shop;

    [Tooltip("Cooldown v sekundách")]
    public float cooldown = 30f;

    float lastOpenTime = -999f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time - lastOpenTime < cooldown) return;

        lastOpenTime = Time.time;
        shop.OpenShop();
    }
}
