using UnityEngine;

public class BasicMovementScript : MonoBehaviour
{
    public float maxSpeed;
    public float speedByHealthMultiplier;
    private float speed;
    private Rigidbody rb;
    [SerializeField]private SOFloatVar healthSO;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        speed = maxSpeed + ((1 - Mathf.InverseLerp(0, 100, healthSO.value)) * speedByHealthMultiplier);
        Debug.Log(speed);
    }

    private void FixedUpdate() {
        int forward = 0;
        int side = 0;

        if (Input.GetKey(KeyCode.W))
        {
            forward = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forward = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            side = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            side = -1;
        }

        rb.linearVelocity = new Vector3(forward * speed, 0, side * speed);
    }
}
