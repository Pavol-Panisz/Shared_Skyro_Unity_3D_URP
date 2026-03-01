using System.Collections.Generic;
using UnityEngine;

public class TestScrippt : MonoBehaviour
{
    [SerializeField]private List<Vector3> vectorList = new List<Vector3>();
    [SerializeField]private Vector3 startDir;
    [SerializeField]private Vector3 endDir;
    [SerializeField]private Vector3 pos;
    [SerializeField]private float multiplier;

    void Update()
    {
        /*Debug.DrawRay(transform.position, startDir * multiplier, Color.red, 0.1f);
        Debug.DrawRay(transform.position, endDir * multiplier, Color.blue, 0.1f);
        Debug.DrawRay(transform.position, ((endDir * multiplier) + (startDir * multiplier)) / 2, Color.blue, 0.1f);
        Debug.DrawRay(transform.position, pos + (((endDir * multiplier) + (startDir * multiplier)) / 2), Color.yellow, 0.1f);
        Debug.DrawLine(transform.position, transform.position + pos, Color.green, 0.1f);*/

        Vector3 avg = Vector3.zero;
        foreach (Vector3 vector in vectorList)
        {
            avg += vector;
        }

        avg = avg / vectorList.Count;
        Debug.DrawLine(transform.position, transform.position + avg, Color.green, 0.1f);
    }   

    void OnDrawGizmos()
    {
        
    }   
}
