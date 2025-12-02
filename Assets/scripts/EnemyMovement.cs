using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 whereToMove;
    [SerializeField] float timeToTravel;
    [SerializeField] Rigidbody rb;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawSphere(transform.position + whereToMove, 0.2f);
    }

    void Start()
    {
        //transform.right = whereToMove;
        MoveEnenmy();
    }

    void Update()
    {
        
    }

    private void MoveEnenmy()
    {
        rb.linearVelocity = whereToMove / timeToTravel;
        Debug.Log(rb.linearVelocity);
        //Invoke(nameof(MoveEnenmy), timeToTravel);
    }
}
