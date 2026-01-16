using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("=== BULLET SETTINGS ===")]
    [Tooltip("Time in seconds before bullet is automatically destroyed")]
    public float lifetime = 3f;

    [Tooltip("Should bullet be destroyed when hitting anything?")]
    public bool destroyOnAnyCollision = true;

    [Tooltip("Particle effect when destroyed")]
    public GameObject destroyEffect;

    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;

        // Auto-destroy after lifetime
        if (lifetime > 0)
        {
            Destroy(gameObject, lifetime);
        }
    }

    void Update()
    {
        // Optional: Check lifetime manually
        if (lifetime > 0 && Time.time - spawnTime > lifetime)
        {
            DestroyBullet();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnAnyCollision)
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}