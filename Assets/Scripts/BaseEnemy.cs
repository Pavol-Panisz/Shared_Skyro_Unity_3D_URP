using UnityEngine;

public abstract class BaseEnemy : Character
{
    [SerializeField] protected float attackRadius;
    
    private void Update()
    {
        Chase();
        
        if (IsCloseEnoughToAttack())
            Attack();
    }

    private bool IsCloseEnoughToAttack()
        => Vector2.Distance(transform.position, Player.Instance.transform.position) <= attackRadius;

    protected virtual void Chase()
    {
        var direction = (Player.Instance.transform.position - transform.position).normalized;

        transform.position += direction * (walkSpeed * Time.deltaTime);
    }
}