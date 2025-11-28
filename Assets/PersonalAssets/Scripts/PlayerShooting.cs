using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public int currentAmmo = 10;
    public int maxAmmo = 10;
    public bool isReloading = false;
    public float reloadingSpeed = 0.5f;
    public float shootingSpeed = 1f;

    void Start()
    {
        if (currentAmmo != maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // if (Input.GetKey(KeyCode.U))
        // {
        //     StartCoroutine(Reload());
        // }

        if (Input.GetKeyDown(KeyCode.U) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0 && isReloading == false)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.linearVelocity = firePoint.forward * projectileSpeed;

            currentAmmo--;

            Destroy(projectile, 3f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        while (currentAmmo < maxAmmo)
        {
            yield return new WaitForSeconds(reloadingSpeed);
            currentAmmo++;
        }

        isReloading = false;
    }
}

/*
old reload system:

IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(maxAmmo - currentAmmo);

        currentAmmo = maxAmmo;

        isReloading = false;

        yield return 0; // stop corot
    }

*/
