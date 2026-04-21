using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Hitbox
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (IsThereAPlayer(other, out HealthSystem healthSystem, out bool isAttacker))
        {
            if (isAttacker) return;
            healthSystem.Damage(_damage);
        }

        Destroy(gameObject);
    }

    public static Projectile SpawnProjectile(GameObject projectilePrefab, Vector3 spawnPos, Vector3 forwardVector, float damage, float lifeTime, float speed, HealthSystem attacker)
    {
        GameObject projectileObject = Instantiate(projectilePrefab);

        projectileObject.transform.position = spawnPos;
        projectileObject.transform.forward = forwardVector;

        if (projectileObject.TryGetComponent(out Projectile projectileScript))
        {
            projectileScript._damage = damage;
            projectileScript._lifeTime = lifeTime;
            projectileScript._attacker = attacker;
        }

        if(projectileObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.linearVelocity = forwardVector.normalized * speed;
        }

        return projectileScript;
    }
}
