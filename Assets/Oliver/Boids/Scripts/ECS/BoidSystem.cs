using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;
using Unity.Entities.UniversalDelegates;

public partial struct BoidSystem : ISystem
{
    //Boid Settings
    public float boidSpeed;
    public float randomPosDist;
    public float seeRadius;
    public float rotationSpeed;

    //Separation Settings
    public bool separationEnabled;
    public float separationDistance;
    public float separationMultiplier;

    //Aligment Settings
    public bool aligmentEnabled;
    public float aligmentMultiplier;

    //Cohesion Settings
    public bool cohesionEnabled;

    //Settings
    public bool debugSeparationDirection;
    public bool debugAligmentDirection;
    public bool debugCenterOfMass;
    public bool debugTargetDirection;
    public bool debugSeeRadius;
    public bool debugRaycast;

    //References
    public NativeArray<LocalTransform> boids;
    /*public NativeArray<LocalTransform> firstboids;
    public NativeArray<LocalTransform> secondboids;*/

    public void OnCreate(ref SystemState state)
    {
        SetupVariables();
    }

    private void SetupVariables()
    {
        boidSpeed = 2f;
        randomPosDist = 10f;
        seeRadius = 4f;
        rotationSpeed = 0.5f;

        separationEnabled = true;
        separationDistance = 1;
        separationMultiplier = 2000;

        aligmentEnabled = true;
        aligmentMultiplier = 500;

        cohesionEnabled = true;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (boids.Length <= 0)
        {
            EntityQuery query = SystemAPI.QueryBuilder()
                .WithAll<LocalTransform>()
                .Build();

            boids = query.ToComponentDataArray<LocalTransform>(Allocator.Persistent);

            /*firstboids = new NativeArray<LocalTransform>(boids.Length / 2, Allocator.Persistent);
            secondboids = new NativeArray<LocalTransform>(boids.Length / 2, Allocator.Persistent);

            for (int i = 0; i < boids.Length; i++)
            {
                if (i < boids.Length / 2)
                {
                    firstboids[i] = boids[i];
                }
                else
                {
                    secondboids[boids.Length - i] = boids[i];
                }
            }*/
        }

        BoidCalculationJob boidCalculationJob = new BoidCalculationJob
        {
            boidSpeed = boidSpeed,
            randomPosDist = randomPosDist,
            seeRadius = seeRadius,
            rotationSpeed = rotationSpeed,

            separationEnabled = separationEnabled,
            separationDistance = separationDistance,
            separationMultiplier = separationMultiplier,

            aligmentEnabled = aligmentEnabled,
            aligmentMultiplier = aligmentMultiplier,

            cohesionEnabled = cohesionEnabled,

            deltaTime = SystemAPI.Time.DeltaTime,

            boids = boids

        };
        boidCalculationJob.Schedule();
    }

    [BurstCompile]
    private static float3 CalculateCenterOfMass(LocalTransform localTransform, NativeArray<LocalTransform> boids, float seeRadius)
    {
        float3 centerOfMass = float3.zero;
        int index = 0;

        foreach (LocalTransform boid in boids)
        {
            if (math.distance(boid.Position, localTransform.Position) < seeRadius)
            {
                centerOfMass += boid.Position;
                index++;
            }
        }

        centerOfMass = centerOfMass / index;
        //Debug.DrawLine(centerOfMass, new float3(centerOfMass.x, centerOfMass.y + 0.5f, centerOfMass.z), Color.red, 0.1f);

        return centerOfMass;
    }

    private static bool IsEqual(float3 first, float3 second)
    {
        if (first.x != second.x) return false;
        if (first.y != second.y) return false;
        if (first.z != second.z) return false;

        return true;
    }

    [BurstCompile]
    public partial struct BoidCalculationJob : IJobEntity
    {
        //Boid Settings
        public float boidSpeed;
        public float randomPosDist;
        public float seeRadius;
        public float rotationSpeed;

        //Separation Settings
        public bool separationEnabled;
        public float separationDistance;
        public float separationMultiplier;

        //Aligment Settings
        public bool aligmentEnabled;
        public float aligmentMultiplier;

        //Cohesion Settings
        public bool cohesionEnabled;

        //Time
        public float deltaTime;

        //References
        public NativeArray<LocalTransform> boids;

        [BurstCompile]
        public void Execute(ref LocalTransform localTransform)
        {
            float dist;
            float3 centerOfMass = float3.zero;
            float3 separationDir = float3.zero;
            float3 aligmentDir = float3.zero;
            float3 target = float3.zero;

            NativeArray<float3> aligmentDirs = new NativeArray<float3>(boids.Length, Allocator.Temp);
            NativeArray<float3> separationDirs = new NativeArray<float3>(boids.Length, Allocator.Temp);

            target = float3.zero;

            //Calculate Values
            dist = math.distance(localTransform.Position, target);

            centerOfMass = CalculateCenterOfMass(localTransform, boids, seeRadius);

            separationDir = float3.zero;
            aligmentDir = float3.zero;

            for (int i = 0; i < aligmentDirs.Length; i++)
            {
                aligmentDirs[i] = 0;
                separationDirs[i] = 0;
            }

            if (cohesionEnabled)
            {
                target = centerOfMass;
            }

            int a = 0;
            if (separationEnabled)
            {
                //Get all directions around
                foreach (LocalTransform boid in boids)
                {
                    if (boid.Position.x == localTransform.Position.x) continue;
                    if (math.distance(localTransform.Position, boid.Position) < separationDistance)
                    {
                        separationDirs[a] = boid.Position - localTransform.Position;
                    }
                }

                if (separationDirs.Length > 0)
                {
                    foreach (float3 dir in separationDirs)
                    {
                        separationDir += dir;
                    }
                    separationDir = -(separationDir / separationDirs.Length);    
                }
            }

            if (aligmentEnabled)
            {
                a = 0;
                //Get all directions around
                foreach (LocalTransform boid in boids)
                {
                    if (IsEqual(boid.Position, localTransform.Position)) continue;
                    if (math.distance(localTransform.Position, boid.Position) < seeRadius)
                    {
                        aligmentDirs[a] = boid.Forward();
                    }
                }

                if (aligmentDirs.Length > 0)
                {
                    foreach (float3 dir in aligmentDirs)
                    {
                        aligmentDir += dir;
                    }
                    aligmentDir = aligmentDir / aligmentDirs.Length;
                }
            }

            if (IsEqual(aligmentDir + (separationDir * separationMultiplier), float3.zero))
            {
                target = localTransform.Position + localTransform.Forward();
            }
            else
            {
                target += (aligmentDir * aligmentMultiplier) + (separationDir * separationMultiplier);
            }

            //Change Rot
            localTransform.Rotation = Quaternion.Slerp(localTransform.Rotation, quaternion.LookRotationSafe(math.normalize(target - localTransform.Position), localTransform.Up()), deltaTime * rotationSpeed);
            
            //Set Velocity
            localTransform.Position = localTransform.Position + (localTransform.Forward() * boidSpeed * deltaTime);
            
            if (localTransform.Position.y > randomPosDist) localTransform.Position = new float3(localTransform.Position.x, -randomPosDist, localTransform.Position.z);
            if (localTransform.Position.y < -randomPosDist) localTransform.Position = new float3(localTransform.Position.x, randomPosDist, localTransform.Position.z);

            if (localTransform.Position.x > randomPosDist) localTransform.Position = new float3(-randomPosDist, localTransform.Position.y, localTransform.Position.z);
            if (localTransform.Position.x < -randomPosDist) localTransform.Position = new float3(randomPosDist, localTransform.Position.y, localTransform.Position.z);

            if (localTransform.Position.z > randomPosDist) localTransform.Position = new float3(localTransform.Position.x, localTransform.Position.y, -randomPosDist);
            if (localTransform.Position.z < -randomPosDist) localTransform.Position = new float3(localTransform.Position.x, localTransform.Position.y, randomPosDist);
        }
    }
}
