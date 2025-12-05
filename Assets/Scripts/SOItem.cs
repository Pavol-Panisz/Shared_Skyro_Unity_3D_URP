
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SOItem : ScriptableObject
{
    public string name;
    public Sprite inventoryImage;

    public string description;

    public int sellingPrice;
}

[CreateAssetMenu]
public class DifficultySetting : ScriptableObject
{
    public int maxPlayerHealth;
    public float basicEnemyDamage;
    public float basicShootingEnemyDamage;

    public float invincibilityCooldown;


}
