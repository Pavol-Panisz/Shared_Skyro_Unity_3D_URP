using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class Boids : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    [SerializeField] Material material;
    [SerializeField] int maxBoids;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField, Min(0.0001f)] float tickDelay;
    [SerializeField, Min(1)] int numNeighbours;
    [SerializeField] float2 repelAngle;
    [SerializeField] float schoolRadius;
    [SerializeField] float agility;


    List <Matrix4x4> fishTRS;
    NativeList<Matrix4x4> fish_container;

    JobHandle handle;
    Unity.Mathematics.Random random = new Unity.Mathematics.Random(1);



    void Awake()
    {
        fishTRS = new List<Matrix4x4>();

        for (int i = 0; i < maxBoids; i++)
        {
            AddFish(Vector3.zero, Quaternion.identity, 1);
        }
        fish_container = new NativeList<Matrix4x4>(1, Allocator.Persistent);
        StartCoroutine(Tick());
    }



    private void OnDestroy()
    {
        handle.Complete();
        if (fish_container.IsCreated)
        {
            fish_container.Dispose();
        }
    }

    private void Update()
    {
        if(fishTRS.Count > 0)
        {
            Graphics.DrawMeshInstanced(mesh, 0, material,fishTRS);
        }
    }

    void AddFish(Vector3 pos, Quaternion rot, float fishSize)
    {
        fishTRS.Add(Matrix4x4.TRS(pos, rot, Vector3.one * fishSize));
    }

    IEnumerator Tick()
    {
        float lastTime = Time.time;
        while (true)
        {
            #region Update Fish Container
            {
                fish_container.SetCapacity(fishTRS.Count);
                NativeArray<Matrix4x4> temp = new NativeArray<Matrix4x4>(fishTRS.ToArray(), Allocator.TempJob);
                fish_container.CopyFrom(temp);
                temp.Dispose();
                yield return new WaitForFixedUpdate();
            }
            #endregion

            #region Update Job
            UpdateJob job = new UpdateJob()
            {
                deltaTime = Time.time - lastTime,
                moveSpeed = speed,
                fish_container = fish_container,
                random = random,
                numNeighbours = numNeighbours,
                repelAngle = repelAngle,
                schoolCenter = transform.position,
                schoolRadius = schoolRadius,
                schoolUp = transform.up,
                agility = agility,
            };

            lastTime = Time.time;

            handle = job.Schedule(fishTRS.Count, 8);
            yield return new WaitUntil(() => handle.IsCompleted);
            handle.Complete();
            #endregion

            #region Update Render List
            Parallel.For(0, fishTRS.Count, (i) =>
            {
                fishTRS[i] = fish_container[i];
            });
            #endregion
            yield return new WaitForSeconds(tickDelay);
        }
    }

    [BurstCompile]
    struct UpdateJob : IJobParallelFor
    {

        [NativeDisableParallelForRestriction] public NativeList<Matrix4x4> fish_container;
        [ReadOnly] public Unity.Mathematics.Random random;
        [ReadOnly] public float moveSpeed;
        [ReadOnly] public float deltaTime;
        [ReadOnly] public int numNeighbours;
        [ReadOnly] public float2 repelAngle;
        [ReadOnly] public float3 schoolCenter;
        [ReadOnly] public float schoolRadius;
        [ReadOnly] public float3 schoolUp;
        [ReadOnly] public float agility;

        public void Execute(int index)
        {
            Matrix4x4 fishTRS = fish_container[index];

            float3 pos = fishTRS.GetPosition();
            Quaternion rot = fishTRS.rotation;
            float3 size = fishTRS.lossyScale;

            float3 fishForward = math.mul(rot, new float3(0,0,1));
            float3 fishUp = math.mul(rot, new float3(0,1,0));
            float3 fishRight = math.mul(rot, new float3(1,0,0));

            pos += fishForward * moveSpeed * deltaTime;

            #region neighbour search
            int startIndex = random.NextInt(0, fish_container.Length);
            int endIndex = random.NextInt(startIndex + 1, startIndex + 1 + numNeighbours);
            endIndex = math.clamp(endIndex, 0, fish_container.Length);

            float totalRepelAngleX = 0;
            float totalRepelAngleY = 0;
            float3 repelDir = 0;

            for(int i = startIndex; i < endIndex; i++)
            {
                Matrix4x4 NeighbourTRS = fish_container[i];
                float3 neighbourPos = NeighbourTRS.GetPosition();

                float distance = math.distance(neighbourPos, pos);
                if(distance < 1e-3f)
                {
                    totalRepelAngleX += repelAngle.x + random.NextFloat(-2, 2);

                    totalRepelAngleY += repelAngle.y + random.NextFloat(-2, 2);
                }
                else
                {
                    float3 fromNeighbourToMeVec = pos - neighbourPos;
                    float d = math.length(fromNeighbourToMeVec);
                    float multiplier = SpecialFunction(d);
                    repelDir += (fromNeighbourToMeVec / d) * multiplier;
                }
            }

            float3 meToSchoolCenter = schoolCenter - pos;
            float disFromSchoolCenter = math.length(meToSchoolCenter);
            float3 meToSchoolCenterDir = meToSchoolCenter / disFromSchoolCenter;

            float goHomeT = disFromSchoolCenter / schoolRadius;
            #endregion

            #region move away from neighbours
            Quaternion targetRot = Quaternion.LookRotation(repelDir + meToSchoolCenterDir, schoolUp) * 
                                    Quaternion.AngleAxis(totalRepelAngleX, fishUp) * 
                                    Quaternion.AngleAxis(totalRepelAngleY, fishRight);

            targetRot = Quaternion.Lerp(
                                        targetRot, 
                                        Quaternion.LookRotation(meToSchoolCenterDir, fishUp),
                                        0.95f * goHomeT);

            rot = Quaternion.Lerp(rot, targetRot, agility);
            #endregion

            fish_container[index] = Matrix4x4.TRS(pos, rot, size);
        }

        float SpecialFunction(float x)
        {
            return 1 / (1 + x);
        }
    }
}
