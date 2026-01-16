using UnityEngine;

public class EnemyPatrolShooter : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Units to move left from the starting X position")] public float leftDistance = 5f;
    [Tooltip("Units to move right from the starting X position")] public float rightDistance = 5f;
    [Tooltip("Movement speed (units/second)")] public float moveSpeed = 2f;
    [Tooltip("Required 3D Rigidbody (Dynamic, no gravity)")] public Rigidbody rb;
    [Tooltip("Pause at turn points (seconds)")] public float pauseDuration = 1f;

    [Header("Shooting")]
    [Tooltip("Spawn point for bullets (position only)")] public Transform firePoint;
    [Tooltip("Bullet prefab with a Bullet component")] public Bullet bulletPrefab;
    [Tooltip("Seconds between shots")] public float shootInterval = 0.5f;
    [Tooltip("Bullet speed (units/second)")] public float bulletSpeed = 12f;
    [Tooltip("Maximum bullets alive at once (per enemy)")] public int maxBullets = 10;
    [Tooltip("Optional parent to organize spawned bullets in hierarchy")] public Transform bulletsParent;

    private float _startX;
    private float _leftX;
    private float _rightX;
    private int _dir = 1; // 1 => +X, -1 => -X
    private bool _isPausing = false;
    private float _pauseUntil = 0f;

    private BulletPool _pool;
    private float _nextShotTime;

    private void Awake()
    {
        _pool = new BulletPool(bulletPrefab, maxBullets, bulletsParent);
    }

    private void Start()
    {
        _startX = transform.position.x;
        _leftX = _startX - Mathf.Abs(leftDistance);
        _rightX = _startX + Mathf.Abs(rightDistance);

        // Random initial direction: -1 or +1
        _dir = Random.value < 0.5f ? -1 : 1;

        if (firePoint == null)
            firePoint = transform;

        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false; // as requested: Dynamic without gravity
            // Consider freezing Y/Z movement and all rotations if needed in Inspector
        }

        _nextShotTime = Time.time + Mathf.Max(0.01f, shootInterval);
    }

    private void Update()
    {
        HandleShooting(); // continuous shooting even during pause
    }

    private void FixedUpdate()
    {
        HandleMovement(Time.fixedDeltaTime);
    }

    private void HandleMovement(float dt)
    {
        if (rb == null)
        {
            // Fallback without Rigidbody (not recommended per spec)
            Vector3 p = transform.position;
            if (_isPausing)
            {
                if (Time.time >= _pauseUntil)
                {
                    _isPausing = false;
                    _dir = -_dir; // change direction after the pause ends
                }
                // No movement while pausing
            }
            else
            {
                p.x += moveSpeed * _dir * dt;
                if (_dir > 0 && p.x >= _rightX)
                {
                    p.x = _rightX;
                    _isPausing = true;
                    _pauseUntil = Time.time + Mathf.Max(0f, pauseDuration);
                }
                else if (_dir < 0 && p.x <= _leftX)
                {
                    p.x = _leftX;
                    _isPausing = true;
                    _pauseUntil = Time.time + Mathf.Max(0f, pauseDuration);
                }
            }
            transform.position = p;
            return;
        }

        // With Rigidbody (preferred)
        Vector3 pos = rb.position;
        Vector3 vel = rb.linearVelocity;

        if (_isPausing)
        {
            vel.x = 0f;
            if (Time.time >= _pauseUntil)
            {
                _isPausing = false;
                _dir = -_dir; // change direction after pause
            }
        }
        else
        {
            vel.x = moveSpeed * _dir;
            float x = pos.x + vel.x * dt; // anticipated position to catch boundary
            if (_dir > 0 && x >= _rightX)
            {
                pos.x = _rightX;
                vel.x = 0f;
                _isPausing = true;
                _pauseUntil = Time.time + Mathf.Max(0f, pauseDuration);
            }
            else if (_dir < 0 && x <= _leftX)
            {
                pos.x = _leftX;
                vel.x = 0f;
                _isPausing = true;
                _pauseUntil = Time.time + Mathf.Max(0f, pauseDuration);
            }
            else
            {
                pos.x += vel.x * dt;
            }
        }

        rb.MovePosition(pos);
        rb.linearVelocity = vel; // keep for external reads; X will be overridden each frame
    }

    private void HandleShooting()
    {
        if (shootInterval <= 0f) return;
        if (Time.time < _nextShotTime) return;

        Bullet b = _pool.Get();
        if (b != null)
        {
            Vector3 dir = new Vector3(_dir, 0f, 0f); // always along X according to movement direction
            b.transform.position = firePoint.position;
            b.gameObject.SetActive(true);
            b.Fire(dir, bulletSpeed);
        }

        _nextShotTime = Time.time + shootInterval;
    }
}
