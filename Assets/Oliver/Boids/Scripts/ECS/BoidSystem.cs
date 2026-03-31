using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;

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
    BoidCalculationJob boidCalculationJob;

    public void OnCreate(ref SystemState state)
    {
        SetupVariables();
    }

    public void OnDestroy(ref SystemState systemState) {
        boids.Dispose();
    }

    private void SetupVariables()
    {
        boidSpeed = 2f;
        randomPosDist = 25f;
        seeRadius = 6f;
        rotationSpeed = .3f;

        separationEnabled = true;
        separationDistance = 1f;
        separationMultiplier = 5;

        aligmentEnabled = true;
        aligmentMultiplier = 4;

        cohesionEnabled = true;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (boids.Length <= 0)
        {
            EntityQuery query = SystemAPI.QueryBuilder()
                .WithAll<LocalTransform/*, BoidScript*/>()
                .Build();

            boids = query.ToComponentDataArray<LocalTransform>(Allocator.Persistent);

            boidCalculationJob = new BoidCalculationJob
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
        }
        
        boidCalculationJob.deltaTime = SystemAPI.Time.DeltaTime;
        boidCalculationJob.ScheduleParallel();
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
            
            float3 centerOfMass = CalculateCenterOfMass(localTransform, boids, seeRadius);
            //float3 centerOfMass = float3.zero;
            float3 separationDir = float3.zero;
            float3 aligmentDir = float3.zero;
            float3 target;
            int separations = 0;
            int aligments = 0;

            //Get all directions around
            foreach (LocalTransform boid in boids)
            {
                if (math.all(boid.Position == localTransform.Position)) continue;

                dist = math.distance(localTransform.Position, boid.Position); 
                
                if (separationEnabled && dist < separationDistance)
                {
                    separationDir += boid.Position - localTransform.Position;
                    separations++;
                }

                if (aligmentEnabled && dist < seeRadius)
                {
                    aligmentDir += boid.Forward();
                    aligments++;
                }
            }

            if (separations > 0)
            {
                separationDir = -(separationDir / separations);
            }
            else
            {
                separationDir = float3.zero;
            }
            if (aligments > 0)
            {
                aligmentDir = aligmentDir / aligments;
            }
            else
            {
                aligmentDir = float3.zero;
            }

            if (cohesionEnabled)
            {
                target = centerOfMass;
            }
            else
            {
                target = localTransform.Position;
            }

            if (math.all((aligmentDir * aligmentMultiplier) + (separationDir * separationMultiplier) == float3.zero) || math.all(centerOfMass == localTransform.Position))
            {
                target = localTransform.Position + localTransform.Forward();
            }
            else
            {
                target += (aligmentDir * aligmentMultiplier) + (separationDir * separationMultiplier);
            }

            //Debug.DrawLine(localTransform.Position, target, Color.red);
            //Debug.DrawLine(localTransform.Position, centerOfMass, Color.green);
            //Debug.DrawRay(localTransform.Position, aligmentDir * aligmentMultiplier, Color.white);

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

        [BurstCompile]
        private float3 CalculateCenterOfMass(LocalTransform localTransform, NativeArray<LocalTransform> boids, float seeRadius)
        {
            int index = 0;
            float3 centerOfMass = float3.zero;

            foreach (LocalTransform boid in boids)
            {
                if (math.distance(boid.Position, localTransform.Position) < seeRadius)
                {
                    centerOfMass += boid.Position;
                    index++;
                }
            }

            if (index > 0)
            {
                centerOfMass = centerOfMass / index;
            }
            else
            {
                centerOfMass = localTransform.Position;
            }
            return centerOfMass;
            //Debug.DrawLine(centerOfMass, new float3(centerOfMass.x, centerOfMass.y + 0.5f, centerOfMass.z), Color.red, 0.1f);
        }
    }
}