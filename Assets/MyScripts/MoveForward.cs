using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float speed;

    void Start()
    {
        myRigidbody.linearVelocity = transform.forward * speed;
    }
}
