using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField, Min(0f)] protected float walkSpeed;
    [SerializeField, Min(0)] protected int maxHealth;
    
    protected int CurrentHealth;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void Damage(int amount)
    {
        CurrentHealth -= amount;
        
        if(CurrentHealth <= 0)
            Die();
    }

    protected abstract void Attack();
    protected abstract void Die();
}