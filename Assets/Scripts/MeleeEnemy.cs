using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] protected DamageDealer damageDealer;
    
    protected override void Attack()
    {
        var angleZ = Vector2.SignedAngle(Vector2.right, RotationDir);
        
        transform.rotation = Quaternion.Euler(0f, 0f, angleZ);
        damageDealer.ExternalAttack();
    }
}