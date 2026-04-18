using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField]private int maxHealth;
    public int health;
    public float moveSpeed;

    [Header("Attack")]
    public float attackSpeed;
    public int damage;
    public bool canAttack = true;

    private void Start()
    {
        CustomStart();
        health = maxHealth;
    }

    public virtual void CustomStart(){}

#region Attack
    public virtual void Attack()
    {
        canAttack = false;
        Invoke(nameof(ResetAttack), attackSpeed);
    }

    public void ResetAttack()
    {
        canAttack = true;
    }
#endregion

#region Health
    private void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    
    public virtual void Die()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }

    void IDamageable.DealDamage(int damage)
    {
        health -= damage;
        CheckHealth();
    }
#endregion
}
