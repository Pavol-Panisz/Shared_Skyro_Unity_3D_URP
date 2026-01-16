using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("=== SHOOTING CONFIGURATION ===")]
    [Tooltip("Bullet prefab to instantiate - Create a bullet prefab and drag it here")]
    public GameObject bulletPrefab;

    [Tooltip("Position where bullets spawn - Create empty GameObject child and drag it here")]
    public Transform firePoint;

    [Tooltip("Target to shoot at (usually Player) - Drag Player GameObject here")]
    public Transform target;

    [Header("=== SHOOTING TIMING ===")]
    [Tooltip("Time between shots in seconds - Lower = faster shooting (0.5 = 2 shots/sec)")]
    public float shootInterval = 1.5f;

    [Tooltip("Random time variation (+/- seconds) to make shooting less predictable")]
    public float intervalVariation = 0.3f;

    [Tooltip("Initial delay before starting to shoot")]
    public float startDelay = 1f;

    [Header("=== BULLET PROPERTIES ===")]
    [Tooltip("Speed of bullets - Higher = faster bullets")]
    public float bulletSpeed = 8f;

    [Tooltip("Accuracy offset in degrees - 0 = perfect aim, 15 = �15 degree spread")]
    public float accuracyOffset = 5f;

    [Tooltip("Bullet lifetime in seconds before auto-destroy")]
    public float bulletLifetime = 4f;

    [Tooltip("Tag to assign to spawned bullets - Important for collision detection")]
    public string bulletTag = "EnemyBullet";

    [Header("=== VISUAL/AUDIO ===")]
    [Tooltip("Muzzle flash particle effect (optional)")]
    public ParticleSystem muzzleFlash;

    [Tooltip("Shooting sound effect (optional)")]
    public AudioClip shootSound;

    [Tooltip("Audio source for shooting sounds (optional)")]
    public AudioSource audioSource;

    private float shootTimer;
    private float nextShotTime;
    private bool canShoot = false;

    void Start()
    {
        // Initialize shooting timer
        nextShotTime = shootInterval + Random.Range(-intervalVariation, intervalVariation);

        // Start shooting after delay
        if (startDelay > 0)
        {
            Invoke(nameof(EnableShooting), startDelay);
        }
        else
        {
            canShoot = true;
        }

        // Try to find player if target not assigned
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                Debug.Log("Auto-found Player as target");
            }
        }

        // Get audio source if not assigned
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!canShoot || target == null || bulletPrefab == null) return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= nextShotTime)
        {
            Shoot();
            shootTimer = 0f;
            nextShotTime = shootInterval + Random.Range(-intervalVariation, intervalVariation);
        }
    }

    void EnableShooting()
    {
        canShoot = true;
    }

    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogWarning("FirePoint not assigned! Using enemy position.");
            firePoint = transform;
        }

        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calculate direction to target with accuracy offset
        Vector3 direction = (target.position - firePoint.position).normalized;

        // Apply accuracy offset
        if (accuracyOffset > 0)
        {
            float angleOffset = Random.Range(-accuracyOffset, accuracyOffset);
            direction = Quaternion.Euler(0, 0, angleOffset) * direction;
        }

        // Setup bullet
        SetupBullet(bullet, direction);

        // Visual/audio effects
        PlayShootEffects();
    }

    void SetupBullet(GameObject bullet, Vector3 direction)
    {
        // Set tag
        bullet.tag = bulletTag;

        // Add bullet controller if needed
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController == null)
        {
            bulletController = bullet.AddComponent<BulletController>();
            bulletController.lifetime = bulletLifetime;
        }

        // Set velocity
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;

            // Rotate bullet to face direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void PlayShootEffects()
    {
        // Muzzle flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Sound
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Draw fire direction in Scene view
        if (firePoint != null && target != null && Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(firePoint.position, target.position);
            Gizmos.DrawWireSphere(firePoint.position, 0.1f);
        }
    }
#endif
}