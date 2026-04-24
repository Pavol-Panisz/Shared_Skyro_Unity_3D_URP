using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField]private int maxHealth;
    public int health;
    public float moveSpeed;

    [Header("Attack")]
    public float attackSpeed;
    public int damage;
    public bool canAttack = true;

    [Header("References")]
    public bool debug;
    public GameObject debugCube;

    private void Start()
    {
        CustomStart();
        health = maxHealth;
    }

    public virtual void CustomStart(){}

#region Attack
    public abstract void Attack();

    public void ResetCanAttack()
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

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
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
