using UnityEngine;

public class SpringScript : MonoBehaviour
{
    #region init
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float strength;
    [SerializeField] float damping;
    [SerializeField] Transform target;
    [SerializeField] private Transform _springVisual;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!_springVisual) Debug.Log("There isnt an spring visual transform.");
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
            springForce) - dampingForceVec3) * Time.fixedDeltaTime;


        Debug.Log(
            $"spring: {springForce.ToString("F2")}" +
            $", vel: {rb.linearVelocity.magnitude.ToString("F2")}" +
            $", damp: {dampingForceVec3.ToString("F2")}"
        );


    }

    private void Update()
    {
        if (_springVisual == null) return;
        Vector3 springDirection = target.position - transform.position;
        Vector3 springVisualTransformPos = transform.position + springDirection / 2f;
        float distance = springDirection.magnitude;

        _springVisual.position = springVisualTransformPos;
        _springVisual.up = springDirection;
        _springVisual.localScale = new Vector3(_springVisual.localScale.x, distance, _springVisual.localScale.z);
    }
}