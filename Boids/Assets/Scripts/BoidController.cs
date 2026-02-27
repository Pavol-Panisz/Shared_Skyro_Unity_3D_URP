using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public List<Rigidbody2D> Boids;

    public Vector2 CenterOfMass;

    void Start()
    {
        if (Boids.Count > 0)
        {
            foreach (Rigidbody2D boid in Boids)
            {
                // Debug.Log(boid.name);

                float Angle = Random.Range(0, 360);

                boid.transform.Rotate(new Vector3(0, 0, Angle));

                boid.linearVelocity = boid.transform.right;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 PositionsCombined = Vector2.zero;

        foreach (Rigidbody2D boid in Boids)
        {
            PositionsCombined += boid.position;
        }

        CenterOfMass = PositionsCombined / Boids.Count;

        foreach (Rigidbody2D boid in Boids)
        {
            Vector2 translation = CenterOfMass - boid.position;

            boid.transform.right = translation;

            boid.linearVelocity = boid.transform.right * 10;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(CenterOfMass, 0.3f);
    }
}
