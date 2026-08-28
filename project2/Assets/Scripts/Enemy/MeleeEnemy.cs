using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public float damage = 10f;
    public GameObject swordHitboxPrefab;
    public float hitboxDuration = 0.2f;

    protected override void Attack()
    {
        base.Attack();
        SpawnSwordHitbox();
    }

    private void SpawnSwordHitbox()
    {
        Vector3 spawnPos = transform.position + transform.forward * 0.8f;
        GameObject hitbox = Instantiate(swordHitboxPrefab, spawnPos, transform.rotation);
        SwordHitbox sword = hitbox.GetComponent<SwordHitbox>();

        if (sword != null)
            sword.Init(damage, hitboxDuration);
        else
            Destroy(hitbox, hitboxDuration);
    }
}
