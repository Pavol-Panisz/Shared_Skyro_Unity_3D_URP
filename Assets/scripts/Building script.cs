using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer)), ExecuteInEditMode()]
public class Buildingscript : MonoBehaviour
{
    private SplineContainer m_splineContainer;

    [SerializeField] Mesh wallMesh;
    [SerializeField] Mesh windowMesh;
    [SerializeField] Mesh doorMesh;

    float distance;

    

    private void Awake()
    {
        m_splineContainer = gameObject.GetComponent<SplineContainer>();
    }

    private void Oalidate()
    {
        if(wallMesh == null)
        {
            return;
        }
        distance = wallMesh.bounds.size.x;

        CalculatePoints();
    }

    private void CalculatePoints()
    {
        
    }
}
