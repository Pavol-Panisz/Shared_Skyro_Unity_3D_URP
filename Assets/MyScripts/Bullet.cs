using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody myRigidbody;

    public float speed;

    private void Start()
    {
        myRigidbody.linearVelocity = transform.forward * speed;
    }
}

