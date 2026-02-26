using System.Collections.Generic;
using UnityEngine;

public class BoidManagerScript: MonoBehaviour
{
    [SerializeField]private float boidSpeed;
    [SerializeField]private float changeTargetDist;
    [SerializeField]private float startSlowingRadius;
    [SerializeField]private float randomPosDist;
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
        foreach (Rigidbody rb in boidsList)
        {
            dist = Vector3.Distance(rb.transform.position, targets[index]);
            slowedSpeed = boidSpeed * dist / startSlowingRadius;
            if (dist < changeTargetDist)
            {
                targets[index] = PickRandomPos();
            }

            rb.transform.LookAt(targets[index]);
            rb.linearVelocity = rb.transform.forward * slowedSpeed;

            index++;
        }        
    }

    private Vector3 PickRandomPos()
    {
        return new Vector3(Random.Range(0f, 1 * randomPosDist), Random.Range(0f, 1 * randomPosDist), Random.Range(0f, 1 * randomPosDist));
    }

    private void SetupTargets()
    {
        for (int i = 0; i < boidsList.Count; i++)
        {
            targets.Add(PickRandomPos());
        }
    }
}
