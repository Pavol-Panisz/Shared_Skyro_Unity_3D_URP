using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public string targetTag = "Enemy";

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag(targetTag))
        {
            Health health = collision.gameObject.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
