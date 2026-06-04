using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField, Min(0f)] protected float moveSpeed;
    
    private void Update()
    {
        var angleInRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        transform.position += new Vector3(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad), 0f) * (moveSpeed * Time.deltaTime);
        
        if(transform.position.sqrMagnitude > 100f)
            Destroy(gameObject);
    }
}