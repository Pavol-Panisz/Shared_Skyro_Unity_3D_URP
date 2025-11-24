using UnityEngine;

public class SpringsDemo : MonoBehaviour
{
    #region init
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float strength;
    [SerializeField] float damping;
    [SerializeField] Transform target;

    [SerializeField] Transform springVisual;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    #endregion init

    void FixedUpdate()
    {
        // calculating velocity vector (only direction)
        Vector3 translation = target.position - transform.position;

        Vector3 directionToTarget = translation.normalized;
        
        // calculating the magnitude of the velocity vector
        // a.k.a the spring force

        float offset = Vector3.Distance(
            transform.position, 
            target.position);

        float springForce = offset * strength;     

        

        // calculate the damping force
        Vector3 dampingForceVec3 = rb.linearVelocity * damping;


        rb.linearVelocity += 
            ((directionToTarget * 
            springForce) - dampingForceVec3)
            * Time.fixedDeltaTime;


        // just debug logging
        Debug.Log(
            $"spring: {springForce.ToString("F2")}" + 
            $", vel: {rb.linearVelocity.magnitude.ToString("F2")}" +
            $", damp: {dampingForceVec3.ToString("F2")}"
        );

        // spring visuals
        //Vector3 newLocalScale = transform.localScale;
        //newLocalScale.z = 10f;
        //transform.localScale = newLocalScale;
    }

    void Update()
    {
        // spring visuals
        Vector3 directionToTarget = 
            (target.position - transform.position).normalized;
     
        springVisual.position = 
            (transform.position + target.position) / 2f;
        
        springVisual.forward = directionToTarget;

        Vector3 visualScale = springVisual.localScale;
        visualScale.z = Vector3.Distance(
            transform.position, target.position
        );
        springVisual.localScale = visualScale;
    }
}
