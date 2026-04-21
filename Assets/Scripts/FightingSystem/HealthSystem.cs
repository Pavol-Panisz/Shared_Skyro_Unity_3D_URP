using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _startHealth = 100f;
    protected virtual float _curHealth { get; set; }

    private void Awake()
    {
        _curHealth = _startHealth;
    }

    public void Damage(float damage)
    {
        _curHealth -= damage;
        if(_curHealth <= 0f)
        {
            _curHealth = 0f;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
