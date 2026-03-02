using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine.UIElements;

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

    public void OnCreate(ref SystemState state)
    {
        SetupVariables();
    }

    private void SetupVariables()
    {
        boidSpeed = 2f;
        randomPosDist = 10f;
        seeRadius = 7.5f;
        rotationSpeed = 0.1f;

        separationEnabled = true;
        separationDistance = 1;
        separationMultiplier = 2;

        aligmentEnabled = true;

        cohesionEnabled = true;
    }

    public void OnUpdate(ref SystemState state)
    {
        int index = 0;
        float dist;
        float3 centerOfMass = float3.zero;
        float3 separationDir = float3.zero;
        float3 aligmentDir = float3.zero;
        float3 target = float3.zero;

        EntityQuery query = SystemAPI.QueryBuilder()
            .WithAll<LocalTransform>()
            .Build();

        boids = query.ToComponentDataArray<LocalTransform>(Allocator.Persistent);
        Debug.Log(boids.Length);

        //foreach ((RefRW<LocalTransform> localTransform, RefRW<PhysicsVelocity> physicsVelocity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<PhysicsVelocity>>())
        foreach (RefRW<LocalTransform> localTransform in SystemAPI.Query<RefRW<LocalTransform>>())
        {
            target = float3.zero;

            //Calculate Values
            dist = math.distance(localTransform.ValueRO.Position, target);

            centerOfMass = CalculateCenterOfMass(localTransform);
            Debug.Log(centerOfMass);

            separationDir = float3.zero;
            aligmentDir = float3.zero;

            NativeArray<float3> aligmentDirs = new NativeArray<float3>(boids.Length, Allocator.Temp);
            NativeArray<float3> separationDirs = new NativeArray<float3>(boids.Length, Allocator.Temp);

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
                    if (boid.Position.x == localTransform.ValueRO.Position.x) continue;
                    if (math.distance(localTransform.ValueRO.Position, boid.Position) < separationDistance)
                    {
                        separationDirs[a] = boid.Position - localTransform.ValueRO.Position;
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
                    if (boid.Position.x == localTransform.ValueRO.Position.x) continue;
                    if (math.distance(localTransform.ValueRO.Position, boid.Position) < seeRadius)
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
                target = localTransform.ValueRO.Position + localTransform.ValueRO.Position;
            }
            else
            {
                target += aligmentDir + (separationDir * separationMultiplier);
            }

            /*if (debugSeparationDirection)
            {
                Debug.DrawRay(localTransform.ValueRO.Position, separationDir, Color.red, 0.1f);
            }
            if (debugTargetDirection)
            {
                    Debug.DrawLine(localTransform.ValueRO.Position, target, Color.blue, 0.1f);
            }
            if (debugAligmentDirection)
            {
                    Debug.DrawRay(localTransform.ValueRO.Position, aligmentDir, Color.green, 0.1f);
            }*/

            //Change Rot
            Debug.DrawLine(localTransform.ValueRO.Position, target);
            localTransform.ValueRW.Rotate(Quaternion.LookRotation(math.normalize(target - localTransform.ValueRO.Position), localTransform.ValueRO.Up()));
            //localTransform.ValueRW.Rotate(Quaternion.LookRotation(Vector3.RotateTowards(localTransform.ValueRO.Forward(), target - localTransform.ValueRO.Forward(), rotationSpeed * Mathf.Deg2Rad, Mathf.Infinity)));

            //Set Velocity
            localTransform.ValueRW.Position = localTransform.ValueRO.Position + (localTransform.ValueRO.Forward() * boidSpeed * Time.deltaTime);
            
            if (localTransform.ValueRO.Position.y > randomPosDist) localTransform.ValueRW.Position = new float3(localTransform.ValueRO.Position.x, -randomPosDist, localTransform.ValueRO.Position.z);
            if (localTransform.ValueRO.Position.y < -randomPosDist) localTransform.ValueRW.Position = new float3(localTransform.ValueRO.Position.x, randomPosDist, localTransform.ValueRO.Position.z);

            if (localTransform.ValueRO.Position.x > randomPosDist) localTransform.ValueRW.Position = new float3(-randomPosDist, localTransform.ValueRO.Position.y, localTransform.ValueRO.Position.z);
            if (localTransform.ValueRO.Position.x < -randomPosDist) localTransform.ValueRW.Position = new float3(randomPosDist, localTransform.ValueRO.Position.y, localTransform.ValueRO.Position.z);

            if (localTransform.ValueRO.Position.z > randomPosDist) localTransform.ValueRW.Position = new float3(localTransform.ValueRO.Position.x, localTransform.ValueRO.Position.y, -randomPosDist);
            if (localTransform.ValueRO.Position.z < -randomPosDist) localTransform.ValueRW.Position = new float3(localTransform.ValueRO.Position.x, localTransform.ValueRO.Position.y, randomPosDist);

            index++;
        }
    }

    private float3 CalculateCenterOfMass(RefRW<LocalTransform> localTransform)
    {
        float3 centerOfMass = float3.zero;
        int index = 0;

        foreach (LocalTransform boid in boids)
        {
            if (math.distance(boid.Position, localTransform.ValueRO.Position) < seeRadius)
            {
                centerOfMass += boid.Position;
                Debug.Log(boid.Position);
                index++;
            }
        }

        centerOfMass = centerOfMass / index;

        return centerOfMass;
    }

    private bool IsEqual(float3 first, float3 second)
    {
        if (first.x != second.x) return false;
        if (first.y != second.y) return false;
        if (first.z != second.z) return false;

        return true;
    }
}
