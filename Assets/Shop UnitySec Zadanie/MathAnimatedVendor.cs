using Unity.Collections;
using UnityEngine;

public class MathAnimatedVendor : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] float maxHeight = 3f;
    [SerializeField] float maxX = 3f;
    [SerializeField] float maxAngle = 45f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // local y position calculation
        float x = Mathf.PingPong(Time.timeSinceLevelLoad, duration);
        float b = duration;
        float h = maxHeight;

        // y = h * (-4 / b^2) * x * (x - b)
        float localY = h * (-4 / (b*b)) * x * (x-b);

        

        
        float worldX = Mathf.Lerp(0, maxX, x / duration) + startPos.x - maxX/2;
        float zAngle = Mathf.Lerp(maxAngle, -maxAngle, x / duration);

        
        // apply changes
        Vector3 pos = transform.position;
        Vector3 rotEuler = transform.rotation.eulerAngles;

        pos.y = localY + startPos.y;
        pos.x = worldX;
        rotEuler.z = zAngle;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(rotEuler);
    }
}
