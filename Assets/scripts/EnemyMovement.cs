using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Vector3 whereToMove;
    [SerializeField] float timeToTravel;
    [SerializeField] Vector3 startLocation;
    float t;
    [SerializeField] Transform playerPosition;
    float distanca;
    [SerializeField] GameObject bullet;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawSphere(transform.position + whereToMove, 0.2f);
    }

    void Start()
    {
        startLocation = transform.position;

        StartCoroutine(ShootBullet());
    }

    void Update()
    {
        t = Mathf.PingPong(Time.timeSinceLevelLoad * timeToTravel, 1);

        Vector3 currentPosition = Vector3.Lerp(startLocation, startLocation + whereToMove, t);

        transform.position = currentPosition;

        transform.LookAt(playerPosition);

        distanca = Vector3.Distance(playerPosition.position, transform.position);

    }

    IEnumerator ShootBullet()
    {
        while (true)
        {
            if (distanca <= 15)
            {   
                StartCoroutine(ShootBullet());
                Instantiate(bullet, transform.position, transform.rotation);
                yield return new WaitForSeconds (1f);
            }
        }
        
        
    }
        
}
