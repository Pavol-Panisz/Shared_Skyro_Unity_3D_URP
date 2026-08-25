using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthComponent))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float mouseSensitivity = 100f;

    private CharacterController _cc;
    private Camera _camera;
    private float _cameraPitch;
    private float _verticalVelocity;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _cameraPitch -= mouseY;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 90f);
        _camera.transform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        // Gravity
        if (_cc.isGrounded)
            _verticalVelocity = -2f;
        else
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;

        move.y = _verticalVelocity;
        _cc.Move(move * walkSpeed * Time.deltaTime);
    }
}