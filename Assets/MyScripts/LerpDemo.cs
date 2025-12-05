using UnityEngine;

public class LerpDemo : MonoBehaviour
{
    [Range(0f, 1f)]
    public float t;

    public Vector3 start;
    public Vector3 end;

    void Update()
    {
        // lerp - linear interpolation

        t = Mathf.PingPong(Time.timeSinceLevelLoad, 1);

        Vector3 currentPosition = Vector3.Lerp(start, end, t);
        
        transform.position = currentPosition;
    }
}
