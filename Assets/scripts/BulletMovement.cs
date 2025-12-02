using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
