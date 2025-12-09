using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float speed;

    void Start()
    {
        myRigidbody.linearVelocity = 
            speed * transform.forward; 
    }

}
