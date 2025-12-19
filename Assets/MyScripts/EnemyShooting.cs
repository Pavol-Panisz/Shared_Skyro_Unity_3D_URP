using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody.linearVelocity = transform.right;
        // StartCoroutine(MovingCarot());

        
    }

    void Update()
    {
        
    }
}
