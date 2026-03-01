using System.Collections.Generic;
using UnityEngine;
public class BoidManagerScript: MonoBehaviour
{
    [Header("Boid Settings")]
    [SerializeField]private float boidSpeed;
    [SerializeField]private float changeTargetDist;
    [SerializeField]private float startSlowingRadius;
    [SerializeField]private float randomPosDist;
    [SerializeField]private float seeRadius;
    [SerializeField]private float rotationSpeed;

    [Header("Separation Settings")]
    [SerializeField]private bool separationEnabled;
    [SerializeField]private float separationDistance;
    [SerializeField]private float separationMultiplier = 2f;

    [Header("Aligment Settings")]
    [SerializeField]private bool aligmentEnabled;

    [Header("Cohesion Settings")]
    [SerializeField]private bool cohesionEnabled;

    [Header("Raycast Settings")]
    [SerializeField]private LayerMask obstacleLayerMask;
    [SerializeField]private float sphereCastRadius;

    [Header("Chunks")]

    [Header("Settings")]
    [SerializeField]private bool debugSeparationDirection;
    [SerializeField]private bool debugAligmentDirection;
    [SerializeField]private bool debugCenterOfMass;
    [SerializeField]private bool debugTargetDirection;
    [SerializeField]private bool debugSeeRadius;
    [SerializeField]private bool debugRaycast;
    [SerializeField ]private List<Color> colors = new List<Color>();

    [Header("References")]
    [SerializeField]private Transform target;
    [SerializeField]private List<Rigidbody> boidsList = new List<Rigidbody>();
    [SerializeField]private GameObject boidPrefab;
    private List<Vector3> targets = new List<Vector3>();

    void Awake()
    {
        SetupTargets();
    }

    void Update()
    {
        ControlBoids();
    }

    private void ControlBoids()
    {
        int index = 0;
        float dist;
        Vector3 centerOfMass = Vector3.zero;
        Vector3 separationDir = Vector3.zero;
        Vector3 aligmentDir = Vector3.zero;

        foreach (Rigidbody rb in boidsList)
        {
            //Calculate Values
            dist = Vector3.Distance(rb.transform.position, targets[index]);

            centerOfMass = CalculateCenterOfMass(rb);

            separationDir = Vector3.zero;
            aligmentDir = Vector3.zero;

            List<Vector3> aligmentDirs = new List<Vector3>();
            List<Vector3> separationDirs = new List<Vector3>();
            RaycastHit hit;
            if (Physics.SphereCast(rb.transform.position, sphereCastRadius, rb.transform.forward, out hit, seeRadius, obstacleLayerMask))
            {
                targets[index] = rb.transform.right * 5;
                if (debugRaycast)
                {
                    Debug.DrawLine(rb.transform.position, hit.point, Color.white, 0.25f);
                }
            }
            else
            {
                if (cohesionEnabled)
                {
                    targets[index] = centerOfMass;
                }

                if (separationEnabled)
                {
                    //Get all directions around
                    foreach (Rigidbody boid in boidsList)
                    {
                        if (boid == rb) continue;
                        if (Vector3.Distance(rb.transform.position, boid.transform.position) < separationDistance)
                        {
                            separationDirs.Add(boid.transform.position - rb.transform.position);
                        }
                    }

                    if (separationDirs.Count > 0)
                    {
                        foreach (Vector3 dir in separationDirs)
                        {
                            separationDir += dir;
                        }
                        separationDir = -(separationDir / separationDirs.Count);    
                    }
                }

                if (aligmentEnabled)
                {
                    //Get all directions around
                    foreach (Rigidbody boid in boidsList)
                    {
                        if (boid == rb) continue;
                        if (Vector3.Distance(rb.transform.position, boid.transform.position) < seeRadius)
                        {
                            aligmentDirs.Add(boid.transform.forward);
                        }
                    }

                    if (aligmentDirs.Count > 0)
                    {
                        foreach (Vector3 dir in aligmentDirs)
                        {
                            aligmentDir += dir;
                        }
                        aligmentDir = aligmentDir / aligmentDirs.Count;
                    }
                }
                
                if (aligmentDir + (separationDir * separationMultiplier) == Vector3.zero)
                {
                    targets[index] = rb.transform.position + rb.transform.forward;
                }
                else
                {
                    targets[index] += aligmentDir + (separationDir * separationMultiplier);
                }
            }

            if (debugSeparationDirection)
            {
                Debug.DrawRay(rb.transform.position, separationDir, Color.red, 0.1f);
            }
            if (debugTargetDirection)
            {
                Debug.DrawLine(rb.transform.position, targets[index], colors[index], 0.1f);
            }
            if (debugAligmentDirection)
            {
                Debug.DrawRay(rb.transform.position, aligmentDir, Color.green, 0.1f);
            }

            //Change Rot
            rb.transform.forward = Vector3.RotateTowards(rb.transform.forward, targets[index] - rb.transform.position, rotationSpeed * Mathf.Deg2Rad, Mathf.Infinity);

            //Set Velocity
            rb.linearVelocity = rb.transform.forward * boidSpeed;

            if (rb.transform.position.y > randomPosDist) rb.transform.position = new Vector3(rb.transform.position.x, -randomPosDist, rb.transform.position.z);
            if (rb.transform.position.y < -randomPosDist) rb.transform.position = new Vector3(rb.transform.position.x, randomPosDist, rb.transform.position.z);

            if (rb.transform.position.x > randomPosDist) rb.transform.position = new Vector3(-randomPosDist, rb.transform.position.y, rb.transform.position.z);
            if (rb.transform.position.x < -randomPosDist) rb.transform.position = new Vector3(randomPosDist, rb.transform.position.y, rb.transform.position.z);

            if (rb.transform.position.z > randomPosDist) rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -randomPosDist);
            if (rb.transform.position.z < -randomPosDist) rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, randomPosDist);

            index++;
        }
    }

    private Vector3 CalculateCenterOfMass(Rigidbody boidRb)
    {
        Vector3 centerOfMass = Vector3.zero;
        int index = 0;
        foreach (Rigidbody rb in boidsList)
        {
            if (Vector3.Distance(rb.transform.position, boidRb.transform.position) < seeRadius)
            {
                centerOfMass += rb.transform.position;
                index++;
            }
        }

        centerOfMass = centerOfMass / index;

        return centerOfMass;
    }

    private Vector3 PickRandomPos()
    {
        return new Vector3(Random.Range(0f, 1 * randomPosDist), Random.Range(0f, 1 * randomPosDist), Random.Range(0f, 1 * randomPosDist));
    }

    private void SetupTargets()
    {
        for (int i = 0; i < boidsList.Count; i++)
        {   
            targets.Add(target.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(randomPosDist, randomPosDist, randomPosDist) * 2);

        if (!debugCenterOfMass && !debugSeeRadius) return;

        int index = 0;
        foreach (Rigidbody rb in boidsList)
        {
            Gizmos.color = colors[index];

            if (debugSeeRadius)
            {
                Gizmos.DrawWireSphere(rb.transform.position, seeRadius);
            }
            if (debugCenterOfMass)
            {
                Gizmos.DrawWireSphere(CalculateCenterOfMass(rb), 0.5f);
            }

            index++;
        }

        
    }

    [ContextMenu("Randomize Colors")]
    private void RandomizeColors()
    {
        for (int i = 0; i < boidsList.Count; i++)
        {
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            colors.Add(color);
        }
    }

    [ContextMenu("Randomize Rotations")]
    private void RandomizeRotations()
    {
        foreach (Rigidbody boid in boidsList)
        {
            boid.transform.rotation = Random.rotation;
        }
    }

    [ContextMenu("Reset Rotations")]
    private void ResetRotations()
    {
        foreach (Rigidbody boid in boidsList)
        {
            boid.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    [ContextMenu("Randomize Positions")]
    private void RandomizePositions()
    {
        foreach (Rigidbody boid in boidsList)
        {
            boid.transform.position = new Vector3(Random.Range(-randomPosDist, randomPosDist), Random.Range(-randomPosDist, randomPosDist), Random.Range(-randomPosDist, randomPosDist));
        }
    }

    [ContextMenu("Fill Boid List")]
    private void FillBoidList()
    {
        if (!boidPrefab) Debug.LogError("Boid prefab is not assigned!!!");

        for (int i = 0; i < boidsList.Count; i++)
        {
            if (boidsList[i] == null)
            {
                boidsList[i] = Instantiate(boidPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            }
        }
    }
}
