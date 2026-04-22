using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] protected float _startHealth = 100f;
    protected virtual float _curHealth { get; set; }

    private void Awake()
    {
        _curHealth = _startHealth;
    }

    public virtual void Damage(float damage)
    {
        _curHealth -= damage;
        if(_curHealth <= 0f)
        {
            _curHealth = 0f;
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}

public interface IDamagable
{
    void Damage(float damage);
}