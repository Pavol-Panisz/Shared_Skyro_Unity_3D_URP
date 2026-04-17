using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] protected Transform bulletPrefab;
    
    protected override void Attack()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}