using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackScriptableObject", menuName = "Fightnig System/Create new Ranged Attack Scriptable Object")]
public class RangedAttackScriptableObject : AttackScriptableObject
{
    public GameObject projectilePrefab;
    public float projectileLifeTime;
    public float projectileSpeed;
}
