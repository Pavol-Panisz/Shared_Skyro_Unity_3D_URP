using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out IDamageable damageable);
        if (damageable != null)
        {
            damageable.DealDamage(1);
        }

        Destroy(gameObject);
    }
}
