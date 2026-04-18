using UnityEngine;

public class MeleeEnemy : BaseEnemyClass
{
    public override void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward, Vector3.one / 2);
        IDamageable damageable;
        foreach (Collider collider in colliders)
        {
            if (collider.transform == transform) continue;
            collider.TryGetComponent(out damageable);

            if (damageable != null) damageable.DealDamage(damage);
        }

        base.Attack();
    }
}
