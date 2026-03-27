using UnityEngine;

public class RangedEnemy : EnemyBase
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 8f;

    protected override void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;

        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.linearVelocity = (player.position - shootPoint.position).normalized * projectileSpeed;
    }
}