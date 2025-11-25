// fixed code for springs. Make sure the rigidbody itself, in the editor,  has linearDamping set to 0

using UnityEngine;

public class SpringsDemo : MonoBehaviour
{
    #region init
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float strength;
    [SerializeField] float damping;
    [SerializeField] Transform target;
    [SerializeField] AnimationCurve xyScaleByDistance;
    [SerializeField] GameObject middleCube;
    [SerializeField] GameObject mainCube;
    [SerializeField] LayerMask ground;
    Rigidbody middleRB;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        middleRB = middleCube.GetComponent<Rigidbody>();

    }
    #endregion init

    void FixedUpdate()
    {
        // calculating velocity vector (only direction)
        Vector3 translation = target.position - transform.position;
        Vector3 directionToTarget = translation.normalized;

        float offset = Vector3.Distance(transform.position,target.position);
        float springForce = offset * strength;

        // calculate the damping force
        Vector3 dampingForceVec3 = rb.linearVelocity * damping;

        rb.linearVelocity += ((directionToTarget * springForce) - dampingForceVec3)* Time.fixedDeltaTime;


        Debug.Log($"spring: {springForce.ToString("F2")}" + $", vel: {rb.linearVelocity.magnitude.ToString("F2")}" + $", damp: {dampingForceVec3.ToString("F2")}");


    }
    private void Update()
    {
        Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool rayCastHit = Physics.Raycast(rayCast, out RaycastHit hit, Mathf.Infinity, ground);
        if (rayCastHit)
        {
            Vector3 raycastHitPoint = hit.point;
            mainCube.transform.position = raycastHitPoint;
        }


        Vector3 translation = target.position - transform.position;
        Vector3 directionToTarget = translation.normalized;
        Vector3 something = (transform.position + target.position) / 2;

        middleCube.transform.LookAt(target, Vector3.right);
        middleCube.transform.position = something;

        float dist = Vector3.Distance(transform.position, target.position);

        middleCube.transform.localScale = new Vector3(xyScaleByDistance.Evaluate(dist), xyScaleByDistance.Evaluate(dist), dist);
        middleCube.transform.position = (transform.position + target.position) / 2f;
    }
}