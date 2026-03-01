using UnityEngine;

public class PointsOnSphereTester : MonoBehaviour
{
    [SerializeField]private int amountOfPoints;
    [SerializeField]private float radius;
    [SerializeField]private int highlightedPoints;
    [SerializeField]private float gizmoSphereRadius = 0.05f;

    void OnDrawGizmos()
    {
        Vector3[] points = BoidHelperScript.Generate(amountOfPoints, radius);

        int index = 0;
        foreach (Vector3 point in points)
        {
            if (index <= highlightedPoints) Gizmos.color = Color.red;
            else Gizmos.color = Color.white;

            Gizmos.DrawWireSphere(point, gizmoSphereRadius);

            index++;
        }
    }
}
