using UnityEngine;

public class EnemyFightingSystem : FightingSystem
{
    protected override AttackScript SpawnAttack(AttackScriptableObject attackScriptableObject)
    {
        AttackScript attackScript = base.SpawnAttack(attackScriptableObject);

        if (attackScript != null)
        {
            SetTargetToTheRangedWeapon(attackScript, PlayerMovement.Instance != null ? PlayerMovement.Instance.transform : FindObjectOfType<PlayerMovement>().transform);
        }

        return attackScript;
    }
}
