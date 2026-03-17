using System.Collections.Generic;
using UnityEngine;

public class Agents : MonoBehaviour
{
    ComputeBuffer positionsBuffer;
    [SerializeField] int agentsCount = 10;
    [SerializeField] float spawnRadius;
    [SerializeField] float groundOffset = 1f;
    [SerializeField] Rigidbody agentPrefab;
    [SerializeField] float agentSpeed = 5f;
    [SerializeField] float agentRadius = 5f;

    [SerializeField] [Range(0f, 1f)]
    float separationWeight = 1f;
    [SerializeField] [Range(0f, 1f)]
    float cohesionWeight = 1f;

    List<Rigidbody> agents = new List<Rigidbody>();


    void Start()
    {
        for (int i = 0; i < agentsCount; i++)
        {
            float randX = Random.Range(-spawnRadius, spawnRadius);
            float randZ = Random.Range(-spawnRadius, spawnRadius);
            Vector3 randPos = new Vector3(randX, groundOffset, randZ);

            var agent = Instantiate(agentPrefab, randPos, Quaternion.identity);
            agents.Add(agent);
        }
    }

    void FixedUpdate()
    {
        int count = agents.Count;

        for (int i = 0; i < count; i++)
        {
            Rigidbody agent = agents[i];

            Vector3 separationSum = Vector3.zero;
            Vector3 neighborsPositionSum = Vector3.zero;
            int neighborsCount = 0;

            for (int j = 0; j < count; j++)
            {
                if (i == j)
                    continue;

                Vector3 toOther = agents[j].position - agent.position;
                float distance = toOther.magnitude;

                if (distance <= agentRadius && distance > 0f)
                {
                    separationSum += -toOther.normalized;
                    neighborsPositionSum += agents[j].position;
                    neighborsCount++;
                }
            }

            Vector3 separationForce = Vector3.zero;
            Vector3 cohesionForce = Vector3.zero;

            if (neighborsCount > 0)
            {
                if (separationSum.sqrMagnitude > 0f)
                {
                    separationForce = separationSum.normalized * agentSpeed;
                }

                Vector3 averagePosition = neighborsPositionSum / neighborsCount;
                Vector3 toAveragePosition = averagePosition - agent.position;

                if (toAveragePosition.sqrMagnitude > 0f)
                {
                    cohesionForce = toAveragePosition.normalized * agentSpeed;
                }
            }

            agent.linearVelocity =  separationWeight * separationForce + cohesionWeight * cohesionForce;
        }
    }

    // OnEnable is better than Awake since it suvives hot reloads 
    // (which are possible and desired when doing graphics programming)
    void OnEnable () {

        // parameters are: size (in no particular units), how many bytes each unit has
		positionsBuffer = new ComputeBuffer(agentsCount, 3 * 4);
	}

    void OnDisable () {
		positionsBuffer.Release();
		positionsBuffer = null;
	}
}
