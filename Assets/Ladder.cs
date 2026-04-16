using UnityEngine;

public class Ladder : MonoBehaviour
{
    public PlayerMovement movementScript;
    public LadderMovement ladderMovementScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            movementScript.enabled = false;
            ladderMovementScript.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            movementScript.enabled = true;
            ladderMovementScript.enabled = false;
        }
    }
}
