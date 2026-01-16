using UnityEngine;

public class EnemyMoverShooter : MonoBehaviour
{
    [Header("Movement")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint; // empty child in front of enemy
    public float fireRate = 1f;
    public float bulletSpeed = 10f;

    private Vector3 target;
    private float fireTimer;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    void Shoot()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = transform.right * bulletSpeed;
        }
    }
}