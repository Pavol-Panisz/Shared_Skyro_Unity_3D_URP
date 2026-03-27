using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public float damage = 10f;

    protected override void Attack()
    {
        base.Attack();

        if (Time.time < lastAttackTime + 0.1f) return;

        if (player.TryGetComponent(out IDamageable dmg))
        {
            dmg.TakeDamage(damage);
        }
    }
}