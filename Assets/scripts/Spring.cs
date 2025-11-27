using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] GameObject anchorRef;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject spring;
    [SerializeField] float strenght;
    [SerializeField] float dumping;
    [SerializeField] GameObject cameraRef;
    Vector3 direction;

    void Update()
    {
        direction = (anchorRef.transform.position) - (transform.position);

        spring.transform.position = (anchorRef.transform.position + transform.position) / 2;
        spring.transform.forward = direction;

        Vector3 newLocalScale = spring.transform.localScale;
        newLocalScale.z = Vector3.Distance(spring.transform.position, anchorRef.transform.position) * 2f;
        spring.transform.localScale = newLocalScale;

        /*if (Physics.Raycast(cameraRef.transform.position, 100))
        {
            Debug.Log("Dotklo sa to niecoho");
        }*/

    }
    
    void FixedUpdate()
    {
        rb.linearVelocity += ((direction * strenght) - (rb.linearVelocity * dumping) * Time.fixedDeltaTime);
    }
}
