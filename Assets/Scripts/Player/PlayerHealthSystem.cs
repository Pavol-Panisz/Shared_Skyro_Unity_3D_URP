using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] private Slider _healthSlider;

    public override void Damage(float damage)
    {
        base.Damage(damage);
        _healthSlider.value = _curHealth / _startHealth;
    }

    protected override void Die()
    {
        base.Die();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
