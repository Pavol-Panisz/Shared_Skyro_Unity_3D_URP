using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private float _damage;
    private bool _hasHit;

    public void Init(float damage, float duration)
    {
        _damage = damage;
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHit) return;
        if (!other.TryGetComponent(out IDamageable dmg)) return;
        if (transform.parent != null && other.transform.IsChildOf(transform.parent)) return;

        _hasHit = true;
        dmg.TakeDamage(_damage);
    }
}
