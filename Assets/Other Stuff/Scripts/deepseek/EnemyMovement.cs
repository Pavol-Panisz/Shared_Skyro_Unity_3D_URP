using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [Header("=== PING-PONG MOVEMENT ===")]
    [Tooltip("START point of movement (left/bottom position) - Drag an empty GameObject here")]
    public Transform pointA;

    [Tooltip("END point of movement (right/top position) - Drag another empty GameObject here")]
    public Transform pointB;

    [Tooltip("BASE movement speed when slider is at 1.0 - Actual speed = baseSpeed × speedMultiplier")]
    public float baseSpeed = 3f;

    [Tooltip("UI Slider that controls speed multiplier - Drag a UI Slider here from Canvas")]
    public Slider speedSlider;

    [Tooltip("Current speed multiplier (read from slider)")]
    [SerializeField] private float speedMultiplier = 1f;

    [Tooltip("Current actual movement speed = baseSpeed × speedMultiplier")]
    [SerializeField] private float currentSpeed;

    [Header("=== MOVEMENT VISUALS ===")]
    [Tooltip("Should the enemy face movement direction?")]
    public bool faceMovementDirection = true;

    [Tooltip("Flip sprite when changing direction?")]
    public bool flipSpriteOnDirectionChange = true;

    private Vector3 targetPosition;
    private bool movingToB = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get sprite renderer for flipping
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize movement
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Point A and/or Point B not assigned! Please assign empty GameObjects.");
            return;
        }

        // Start at pointA
        transform.position = pointA.position;
        targetPosition = pointB.position;
        movingToB = true;

        // Set up slider if assigned
        if (speedSlider != null)
        {
            speedSlider.onValueChanged.AddListener(UpdateSpeedMultiplier);
            UpdateSpeedMultiplier(speedSlider.value);
        }
        else
        {
            currentSpeed = baseSpeed;
        }
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        HandleMovement();
    }

    void HandleMovement()
    {
        // Calculate movement step
        float step = currentSpeed * Time.deltaTime;

        // Move towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Handle facing direction
        if (faceMovementDirection && (pointB.position - pointA.position).x != 0)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            if (flipSpriteOnDirectionChange && spriteRenderer != null)
            {
                spriteRenderer.flipX = direction.x < 0;
            }
        }

        // Check if reached target
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Switch target positions (ping-pong)
            if (movingToB)
            {
                targetPosition = pointA.position;
                movingToB = false;
            }
            else
            {
                targetPosition = pointB.position;
                movingToB = true;
            }
        }
    }

    void UpdateSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
        currentSpeed = baseSpeed * speedMultiplier;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Visualize movement points in Scene view
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
#endif
}