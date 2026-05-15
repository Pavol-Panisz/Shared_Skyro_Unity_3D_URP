using System.Collections.Generic;
using UnityEngine;

public class LinkedList : MonoBehaviour
{
    [Header("Room Info")]
    public string roomName;

    [Header("Connected Rooms")]
    public List<LinkedList> connectedRooms = new List<LinkedList>();

    [Header("Visual")]
    public Color nodeColor = Color.cyan;
    public float nodeRadius = 0.5f;

    private void OnDrawGizmos()
    {
        // Draw node
        Gizmos.color = nodeColor;
        Gizmos.DrawSphere(transform.position, nodeRadius);

        // Draw connections
        Gizmos.color = Color.white;

        foreach (LinkedList room in connectedRooms)
        {
            if (room != null)
            {
                Gizmos.DrawLine(transform.position, room.transform.position);
            }
        }
    }
}