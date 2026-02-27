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

    [Header("Cohesion Settings")]
    [SerializeField]private bool cohesionEnabled;

    [Header("Chunks")]

    [Header("Settings")]
    [SerializeField]private DebugType debugType;
    [SerializeField]private bool goToTarget;

    [Header("References")]
    [SerializeField]private Transform target;
    [SerializeField]private List<Rigidbody> boidsList = new List<Rigidbody>();
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
        float slowedSpeed;
        float dist;
        Vector3 centerOfMass;

        foreach (Rigidbody rb in boidsList)
        {
            //Calculate Values
            dist = Vector3.Distance(rb.transform.position, targets[index]);
            slowedSpeed = boidSpeed * dist / startSlowingRadius;
            slowedSpeed = Mathf.Clamp(slowedSpeed, 0, boidSpeed);
            centerOfMass = CalculateCenterOfMass(rb);

            targets[index] = centerOfMass;

            List<Vector3> dirs = new List<Vector3>();
            if (separationEnabled)
            {
                //Get all directions around
                foreach (Rigidbody boid in boidsList)
                {
                    if (boid == rb) continue;
                    if (Vector3.Distance(rb.transform.position, boid.transform.position) < separationDistance)
                    {
                        dirs.Add(boid.transform.position - rb.transform.position);
                    }
                }
            }

            Vector3 finalDir = Vector3.zero;
            if (dirs.Count > 0)
            {
                foreach (Vector3 dir in dirs)
                {
                    finalDir += dir;
                }
                finalDir = -(finalDir / dirs.Count);    
            }
            else if (goToTarget)
            {
                finalDir = targets[index] - rb.transform.position;
            }

            targets[index] += finalDir;

            switch (debugType)
            {
                case DebugType.RawDir:
                    Debug.DrawRay(rb.transform.position, finalDir, Color.red, 0.5f);
                break;
                case DebugType.Target:
                    Debug.DrawLine(rb.transform.position, targets[index], Color.blue, 0.5f);
                break;
                case DebugType.RawDirAndTarget:
                    Debug.DrawRay(rb.transform.position, finalDir, Color.red, 0.5f);
                    Debug.DrawLine(rb.transform.position, targets[index], Color.blue, 0.5f);
                break;
                case DebugType.Everything:
                    Debug.DrawRay(rb.transform.position, finalDir, Color.red, 0.5f);
                    Debug.DrawLine(rb.transform.position, targets[index], Color.blue, 0.5f);
                break;
            }

            //Change Rot
            rb.transform.forward = Vector3.RotateTowards(rb.transform.position, targets[index], rotationSpeed * Mathf.Deg2Rad, Mathf.Infinity);

            //Set Velocity
            rb.linearVelocity = rb.transform.forward * boidSpeed;

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

    void OnDrawGizmos()
    {
        if (debugType == DebugType.Everything)
        {
            foreach (Rigidbody rb in boidsList)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(rb.transform.position, seeRadius);
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(CalculateCenterOfMass(rb), 0.5f);
            }
        }
        if (debugType == DebugType.SeeRadius)
        {
            Gizmos.color = Color.white;
            foreach (Rigidbody rb in boidsList)
            {
                Gizmos.DrawWireSphere(rb.transform.position, seeRadius);
            }
        }
        else if (debugType == DebugType.CenterOfMass)
        {
            Gizmos.color = Color.yellow;
            foreach (Rigidbody rb in boidsList)
            {
                Gizmos.DrawWireSphere(CalculateCenterOfMass(rb), 0.5f);
            }
        }

    }
}

enum DebugType
{
    None,
    RawDir,
    Target,
    RawDirAndTarget,
    CenterOfMass,
    SeeRadius,
    Everything
}
