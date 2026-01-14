using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SOSO : ScriptableObject
{
    public string name;

    public string description;

    public Sprite image;

    public int sellingPrice;

    [SerializeField] float defaultValue;
    public float health;

    void onEnable()
    {
        health = defaultValue;
    }
}
