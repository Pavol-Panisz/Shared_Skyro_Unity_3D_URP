using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float speed;

    void Start()
    {
        if (myRigidbody == null) myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.isKinematic = false;
        myRigidbody.linearVelocity = transform.forward * speed;
    }
}
