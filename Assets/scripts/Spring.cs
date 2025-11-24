using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] GameObject anchorRef;
    [SerializeField] Rigidbody rb;
    [SerializeField] float strenght;
    [SerializeField] float dumping;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (anchorRef.transform.position) - (transform.position);
        rb.linearVelocity += ((direction * strenght) - (rb.linearVelocity * dumping) * Time.fixedDeltaTime);
    }
}
