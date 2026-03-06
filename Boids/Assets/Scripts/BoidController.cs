using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public List<Rigidbody2D> Boids;
    public Vector2 CenterOfMass;

    [Header("Boid Settings")]
    public float Speed = 5f;
    public float NeighborRadius = 3f;
    public float SeparationRadius = 1f;

    [Header("Force Weights")]
    public float CohesionWeight = 1f;
    public float SeparationWeight = 2f;
    public float AlignmentWeight = 1f;

    void Start()
    {
        foreach (Rigidbody2D boid in Boids)
        {
            float angle = Random.Range(0, 360);
            boid.transform.Rotate(new Vector3(0, 0, angle));
            boid.linearVelocity = boid.transform.right * Speed;
        }
    }

    private void FixedUpdate()
    {
        // Recalculate center of mass
        Vector2 combined = Vector2.zero;
        foreach (Rigidbody2D boid in Boids)
		{
			combined += boid.position;
		}
        CenterOfMass = combined / Boids.Count;

        foreach (Rigidbody2D boid in Boids)
        {
            List<Rigidbody2D> neighbors = GetNeighbors(boid);

            Vector2 force = Vector2.zero;
            force += Cohesion(boid, neighbors) * CohesionWeight;
            force += Separation(boid, neighbors) * SeparationWeight;
            force += Alignment(boid, neighbors) * AlignmentWeight;

            // Apply force and keep speed constant
            Vector2 newVelocity = boid.linearVelocity + force * Time.fixedDeltaTime;
            boid.linearVelocity = newVelocity.normalized * Speed;
            boid.transform.right = boid.linearVelocity;
        }
    }

    // Get all boids within NeighborRadius (excluding self)
    private List<Rigidbody2D> GetNeighbors(Rigidbody2D boid)
    {
        List<Rigidbody2D> neighbors = new List<Rigidbody2D>();
        foreach (Rigidbody2D other in Boids)
        {
            if (other == boid) continue;
            if (Vector2.Distance(boid.position, other.position) < NeighborRadius)
                neighbors.Add(other);
        }
        return neighbors;
    }

    // Steer toward average position of neighbors
    private Vector2 Cohesion(Rigidbody2D boid, List<Rigidbody2D> neighbors)
    {
        if (neighbors.Count == 0) return Vector2.zero;

        Vector2 avgPos = Vector2.zero;
        foreach (Rigidbody2D n in neighbors)
            avgPos += n.position;
        avgPos /= neighbors.Count;

        return (avgPos - boid.position).normalized;
    }

    // Steer away from boids that are too close
    private Vector2 Separation(Rigidbody2D boid, List<Rigidbody2D> neighbors)
    {
        Vector2 force = Vector2.zero;
        foreach (Rigidbody2D n in neighbors)
        {
            float dist = Vector2.Distance(boid.position, n.position);
            if (dist < SeparationRadius)
            {
                // The closer they are, the stronger the push
                force += (boid.position - n.position) / dist;
            }
        }
        return force.normalized;
    }

    // Match average velocity of neighbors
    private Vector2 Alignment(Rigidbody2D boid, List<Rigidbody2D> neighbors)
    {
        if (neighbors.Count == 0) return Vector2.zero;

        Vector2 avgVel = Vector2.zero;
        foreach (Rigidbody2D n in neighbors)
            avgVel += n.linearVelocity;
        avgVel /= neighbors.Count;

        return avgVel.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(CenterOfMass, 0.3f);
    }
}