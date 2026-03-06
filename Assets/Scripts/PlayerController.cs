using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public Camera playerCamera;
    public Animator animator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip footstepClip;
    public AudioClip jumpClip;
    public float stepInterval = 0.5f;

    private float xRotation = 0f;
    private Vector3 velocity;
    private CharacterController controller;
    private float stepTimer = 0f;

    public float CurrentSpeed => controller != null ? new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude : 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
        
        if (animator == null)
            animator = GetComponent<Animator>();
            
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Grounded check
        bool isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveInput = transform.right * x + transform.forward * z;
        controller.Move(moveInput * moveSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (animator != null) animator.SetTrigger("Jump");
            if (audioSource != null && jumpClip != null) audioSource.PlayOneShot(jumpClip);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Animations
        bool isWalking = moveInput.magnitude > 0.1f;
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
            animator.SetBool("IsGrounded", isGrounded);
        }

        // Step sounds
        if (isGrounded && isWalking)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                if (audioSource != null && footstepClip != null)
                {
                    audioSource.PlayOneShot(footstepClip);
                }
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval; // Play first step immediately when walking starts
        }
    }
}