using UnityEngine;

public class Bullet : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable;

        if (collision.gameObject.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(5);
        }
    }
}
