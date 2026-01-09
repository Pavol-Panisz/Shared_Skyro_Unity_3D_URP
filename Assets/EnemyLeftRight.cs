using System.Collections;
using UnityEngine;

public class EnemyLeftRight : MonoBehaviour
{
    public Rigidbody myRigidbody;

    void Start()
    {
        // Vector3.right
        // transform.right

        myRigidbody.linearVelocity = transform.right;

        StartCoroutine(MovingCorot());
    }

    IEnumerator MovingCorot()
    {
        yield return new WaitForSeconds(2);

        // WaitForSecond wait_for_second

        
        myRigidbody.linearVelocity = -1 * transform.right;

        

    }
}
