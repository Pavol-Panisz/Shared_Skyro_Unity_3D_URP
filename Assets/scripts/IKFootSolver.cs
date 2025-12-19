using UnityEngine;
using UnityEngine.Splines.Interpolators;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField] Transform body;

    [SerializeField] LayerMask terrainLayer = default;

    [SerializeField] float stepHeight = 1;
    [SerializeField] float speed = 1;
    float footSpacing;
    float lerp;

    Vector3 newPosition;
    Vector3 oldPosition;
    Vector3 currentPosition;

    [SerializeField] float stepDistance = 4;

    void Start()
    {
        footSpacing = transform.localPosition.x;
    }

    void Update()
    {
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
        {
            if(Vector3.Distance(newPosition, info.point) > stepDistance)
            {
                lerp = 0;
                newPosition = info.point;
            }
        }
        if (lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = footPosition;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            newPosition = oldPosition;
        }
    }

    private void ODrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);
    }
}