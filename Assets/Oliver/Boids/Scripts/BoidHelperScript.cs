using System.Collections.Generic;
using UnityEngine;

public static class BoidHelperScript
{
    /// <summary>
    /// theta = rotation around the vertical axis
    /// phi = vertical angle from the top pole
    /// 
    /// x = cos(theta)sin(phi)
    /// y = sin(theta)sin(phi)
    /// z = cos(phi)
    /// </summary>
    public static Vector3[] Generate(int pointAmount, float radius)
    {
        float goldenAngle = Mathf.PI * (1 + Mathf.Sqrt(5));
        Vector3[] points = new Vector3[pointAmount];

        for (int i = 0; i < pointAmount; i++)
        {
            float index = i + 0.5f;

            float phi = Mathf.Acos(1 - 2 * index / pointAmount);
            float theta = goldenAngle * index;

            float x = Mathf.Cos(theta) * Mathf.Sin(phi);
            float y = Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = Mathf.Cos(phi);

            points[i] = new Vector3(x, y, z) * radius;
        }

        return points;
    }

    
}
