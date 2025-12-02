using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MirkovaHra_CameraMovementScript : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;

    [SerializeField] private float _walkSpeed = 10f;
    [SerializeField] private float _runSpeed = 20f;
    private float _curSpeed;
    [SerializeField] private float _mouseSensX = 100f, _mouseSensY = 100f;
    private float _curX, _curY;

    private Vector3 _moveInputs;


    private bool _controlling;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();

        _curSpeed = _walkSpeed;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        SpeedControll();
    }

    private void Movement()
    {
        if (_moveInputs.magnitude < 0.1f || !_controlling) return;
        _rigidbody.AddForce(Camera.main.transform.rotation * _moveInputs * _curSpeed * 100f, ForceMode.Force);
    }

    private void SpeedControll()
    {
        if(_rigidbody.linearVelocity.magnitude > _curSpeed)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _curSpeed;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_controlling) return;
        _moveInputs = context.ReadValue<Vector3>();
        if (_moveInputs.magnitude > 1f) _moveInputs = _moveInputs.normalized;
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!_controlling) return;
        Vector2 rotateInput = context.ReadValue<Vector2>();
        _curX += rotateInput.x * _mouseSensX;
        _curY += rotateInput.y * _mouseSensY;

        transform.rotation = Quaternion.Euler(-_curY, _curX, 0f);
    }
    
    public void OnControll(InputAction.CallbackContext context)
    {
        _controlling = !context.canceled;
        _curSpeed = _walkSpeed;
        _moveInputs = Vector3.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (!_controlling) return;
        _curSpeed = context.canceled ? _walkSpeed : _runSpeed;
    }
}
