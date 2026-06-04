using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] protected Transform bulletPrefab;
    
    protected override void Attack()
    {
        var angleZ = Vector2.SignedAngle(Vector2.right, RotationDir);
        Instantiate(
            bulletPrefab,
            transform.position + RotationDir * closestAttackRange,
            Quaternion.Euler(0f, 0f, angleZ));
    }
}