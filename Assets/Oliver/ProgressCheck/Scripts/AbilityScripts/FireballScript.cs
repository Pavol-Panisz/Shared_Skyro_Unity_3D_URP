using UnityEngine;

public class FireballScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out IDamageable damageable);
        other.gameObject.TryGetComponent(out Player player);

        if (damageable != null && player == null)
        {
            damageable.DealDamage(1);
        }
    }
}
