using UnityEngine;

public class MeleeAttackScript : AttackScript
{
    public override AttackScriptableObject attackScriptableObject { get { return base.attackScriptableObject; } set { base.attackScriptableObject = value; _meleeAttackScriptableObject = value as MeleeAttackScriptableObject; } }
    private MeleeAttackScriptableObject _meleeAttackScriptableObject;

    public override void Attack()
    {
        base.Attack();
        SpawnHitbox();
    }

    private void SpawnHitbox()
    {
        Hitbox hitbox = Hitbox.SpawnHitbox(_meleeAttackScriptableObject.hitboxSize, Hitbox.GetHitboxSpawnPosition(_meleeAttackScriptableObject.hitboxSize, transform.position, transform.forward), transform.forward, attackScriptableObject.attackDamage, _meleeAttackScriptableObject.hitboxLifeTime, attacker);
    }
}
