using UnityEngine;
using System.Collections.Generic;

public class AgentsGPU : MonoBehaviour
{
    #region agent-variables
    public const int agentsCount = 512; // set to const so we dont have to worry about recalculating group and threads count
    public float spawnRadius, agentSpeed, agentRadius;

    public Rigidbody agentPrefab;
    #endregion agent-variables

    public ComputeShader computeShader;
    int kernelId;

    ComputeBuffer positionsComputeBuffer;
    ComputeBuffer resultComputeBuffer;
    List<Rigidbody> agents = new List<Rigidbody>();

    void Start()
    {
        Vector3[] positionsArr = new Vector3[agentsCount];
        Vector3[] resultingVelocities = new Vector3[agentsCount];

        // instantiate agents at random positions and fill the array positionsArr
        for (int i = 0; i < agentsCount; i++)
        {
            float randX = Random.Range(-spawnRadius, spawnRadius);
            float randZ = Random.Range(-spawnRadius, spawnRadius);
            Vector3 randPos = new Vector3(randX, 1.5f, randZ);

            var agent = Instantiate(agentPrefab, randPos, Quaternion.identity);
            agents.Add(agent);

            positionsArr[i] = randPos;
        }


    

        kernelId = computeShader.FindKernel("CSMain");
        Debug.Log(kernelId);

        positionsComputeBuffer = new ComputeBuffer(agentsCount, 3 * 4);  
        resultComputeBuffer = new ComputeBuffer(agentsCount, 3 * 4);

        positionsComputeBuffer.SetData(positionsArr);
        computeShader.SetBuffer(kernelId, "positionsBuffer", positionsComputeBuffer);

        computeShader.SetBuffer(kernelId, "separationForcesBuffer", resultComputeBuffer);
        
        // copy data into the actual compute buffer
        computeShader.SetFloat("agentRadius", agentRadius);
        computeShader.SetFloat("agentSpeed", agentSpeed);

        computeShader.Dispatch(kernelId, 8, 1, 1); // if agentsCount were not 512, we'd have to adjust that here
   
        resultComputeBuffer.GetData(resultingVelocities);

        for (int i = 0; i < agentsCount; i++)
        {
            var agent = agents[i];
            agent.linearVelocity = resultingVelocities[i];
        }
    }

    void OnDisable()
    {
        if (positionsComputeBuffer != null)
        {
            positionsComputeBuffer.Release();
            positionsComputeBuffer = null;
        }

        if (resultComputeBuffer != null)
        {
            resultComputeBuffer.Release();
            resultComputeBuffer = null;
        }
    }
}