using UnityEngine;

public class RangedAttackScript : AttackScript
{
    public override AttackScriptableObject attackScriptableObject { get { return base.attackScriptableObject; } set { base.attackScriptableObject = value; _rangedAttackScriptableObject = value as RangedAttackScriptableObject; } }
    private RangedAttackScriptableObject _rangedAttackScriptableObject;

    public Transform target;

    public override void Attack()
    {
        base.Attack();
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        Hitbox hitbox = Projectile.SpawnProjectile(_rangedAttackScriptableObject.projectilePrefab, transform.position, (target.position - transform.position).normalized, attackScriptableObject.attackDamage, _rangedAttackScriptableObject.projectileLifeTime, _rangedAttackScriptableObject.projectileSpeed, attacker);
    }
}
