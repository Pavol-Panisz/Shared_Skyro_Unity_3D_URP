using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rigidbody myrigidbody;
    public float speed;

    void Start()
    {
        myrigidbody.linearVelocity = transform.forward * speed;
    }
}
