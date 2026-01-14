using UnityEngine;

public class Movement : MonoBehaviour
{
    public float health;
    private Rigidbody rb;
    public SOSO SO_Health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SO_Health.health = health;
        health = Mathf.Clamp(health,0, 100);
    }

    // Update is called once per frame
    void Update()
    {

        float InputX = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(InputX,0,0);
        float speed = health;

        rb.AddForce(dir * speed * Time.deltaTime, ForceMode.Force);
    }
}