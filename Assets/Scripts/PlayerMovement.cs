using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    
    public SOFloatVar healthVar;

    public float maxSpeed;
    public float speedMultiplier;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        
        float speed = speedMultiplier * healthVar.value + maxSpeed;
        rb.linearVelocityX = speed * inputX;

        

    }
}
