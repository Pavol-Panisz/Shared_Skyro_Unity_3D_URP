using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Minion")]
public class ScriptableObjectCard : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artWork;

    public int manaCost;
    public int attack;
    public int health;
}

[CreateAssetMenu(fileName = "New Bla", menuName = "Card/Bla")]
public class ScriptableObjectBla : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artWork;

    public int manaCost;
    public int attack;
    public int health;
}