using UnityEngine;

public class PlayerFightingSystem : FightingSystem
{
    protected override AttackScript SpawnAttack(AttackScriptableObject attackScriptableObject)
    {
        AttackScript attackScript = base.SpawnAttack(attackScriptableObject);
        if (attackScript != null)
        {
            attackScript.gameObject.AddComponent<AttackInputsManager>();
        }

        return attackScript;
    }
}
