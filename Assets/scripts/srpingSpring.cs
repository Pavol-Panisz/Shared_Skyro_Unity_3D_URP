using UnityEngine;

public class srpingSpring : MonoBehaviour
{
    [SerializeField] GameObject anchorRef;
    [SerializeField] GameObject objectOnSpringRef;
    float uhol;

    void Update()
    {
        transform.position = (anchorRef.transform.position + objectOnSpringRef.transform.position) / 2;
        float uhol = Vector3.Angle(Vector3.right, anchorRef.transform.position);
        //transform.forward = ();
    }
}
