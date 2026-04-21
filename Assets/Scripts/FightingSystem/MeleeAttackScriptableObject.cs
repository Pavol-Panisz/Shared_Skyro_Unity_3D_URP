using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackScriptableObject", menuName = "Fightnig System/Create new Melee Attack Scriptable Object")]
public class MeleeAttackScriptableObject : AttackScriptableObject
{
    public Vector3 hitboxSize;
    public float hitboxLifeTime;
}
