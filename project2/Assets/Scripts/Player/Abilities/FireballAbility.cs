using UnityEngine;

public class FireballAbility : AbilityBase
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float speed = 10f;

    protected override void Use()
    {
        GameObject fb = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = fb.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * speed;
    }
}