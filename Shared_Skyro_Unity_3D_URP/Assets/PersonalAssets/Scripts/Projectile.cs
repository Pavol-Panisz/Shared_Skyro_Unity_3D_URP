using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter(Collision collision)
    {

        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
