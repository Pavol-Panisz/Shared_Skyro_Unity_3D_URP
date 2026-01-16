using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float speed = 10f;

    public void Fire(Vector3 dir, float spd)
    {
        direction = dir.sqrMagnitude > 0.0001f ? dir.normalized : Vector3.right;
        speed = Mathf.Max(0f, spd);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
