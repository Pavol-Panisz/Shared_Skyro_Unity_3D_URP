using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 100f;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 15f;
    public float fireRate = 2f;  // time between shots
    public bool aimsAtPlayer = false;  // bonus aimbot toggle

    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // simple ai movement
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // rotate towards player if aimbot is enabled
        if (aimsAtPlayer && player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0;  //keep rotation only on y axis
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime); //https://docs.unity3d.com/ScriptReference/Quaternion.Slerp.html
        }

        // shooting
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (aimsAtPlayer && player != null)
        {
            // goofy aimbot
            Vector3 direction = (player.position - firePoint.position).normalized;
            rb.linearVelocity = direction * projectileSpeed;
        }
        else
        {
            // shoot forward
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }

        Destroy(projectile, 3f);
    }
}
