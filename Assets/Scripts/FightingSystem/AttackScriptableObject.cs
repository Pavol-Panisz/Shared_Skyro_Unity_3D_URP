using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "newAttackScriptableObject", menuName = "Fightnig System/Create new Attack Scriptable Object")]
public class AttackScriptableObject : ScriptableObject
{
    public GameObject attackPrefab;
    public float attackCooldown;
    public float attackDamage;
    public InputAction attackInputAction;
}
