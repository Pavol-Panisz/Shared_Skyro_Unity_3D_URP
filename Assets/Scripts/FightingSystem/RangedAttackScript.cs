using UnityEngine;

public class RangedAttackScript : AttackScript
{
    public override AttackScriptableObject attackScriptableObject { get { return base.attackScriptableObject; } set { base.attackScriptableObject = value; _rangedAttackScriptableObject = value as RangedAttackScriptableObject; } }
    private RangedAttackScriptableObject _rangedAttackScriptableObject;

    [HideInInspector] public Transform target;

    public override void Attack()
    {
        base.Attack();
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        Hitbox hitbox = Projectile.SpawnProjectile(_rangedAttackScriptableObject.projectilePrefab, transform.position, target != null ? (target.position - transform.position).normalized : transform.forward, attackScriptableObject.attackDamage, _rangedAttackScriptableObject.projectileLifeTime, _rangedAttackScriptableObject.projectileSpeed, attacker);
    }
}
