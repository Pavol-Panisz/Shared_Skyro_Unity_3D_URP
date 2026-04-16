using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public float speed = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRb.linearVelocity = Vector3.up * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerRb.linearVelocity = -Camera.main.transform.forward * speed;
        }
        else
        {
            playerRb.linearVelocity = new Vector3(0,0,0);
        }
    }
}
