using UnityEngine;

public class FireballScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent(out IDamageable damageable);
        if (damageable != null && !other.gameObject.GetComponent<Player>())
        {
            damageable.DealDamage(1);
        }

        Destroy(gameObject);
    }
}
