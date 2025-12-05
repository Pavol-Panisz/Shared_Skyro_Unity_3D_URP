using UnityEngine;


public class DialogueDisplay : MonoBehaviour
{
    public Vector3 myVector;

    void Start()
    {
        Debug.Log(
            StaticDialogueLines.dialogueLines[0]
        );
    }
}
