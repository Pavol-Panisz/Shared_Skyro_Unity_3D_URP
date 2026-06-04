using UnityEngine;

public abstract class BaseEnemy : Character
{
    [SerializeField] protected float attackRadius;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float closestAttackRange;
    
    protected Vector3 RotationDir;
    protected float CooldownTimer;
    
    private void Update()
    {
        CalculateRotation();
        Chase();

        if (CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
            return;
        }

        if (!IsCloseEnoughToAttack()) return;
        
        Attack();
        CooldownTimer = cooldown;
    }

    protected virtual void CalculateRotation()
    {
        RotationDir = (GameManager.Player.transform.position - transform.position).normalized;
    }

    protected bool IsCloseEnoughToAttack()
        => Vector2.Distance(transform.position, GameManager.Player.transform.position) <= attackRadius;

    protected virtual void Chase()
    {
        if(Vector2.Distance(GameManager.Player.transform.position, transform.position) > closestAttackRange)
            transform.position += RotationDir * (walkSpeed * Time.deltaTime);
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}