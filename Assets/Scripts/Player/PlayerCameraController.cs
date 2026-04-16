using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerCameraController : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField] private Transform _cameraFollowTransform;

    [SerializeField] private float _horizontalSensitivity = 0.1f, _verticalSensitivity = 0.1f;
    [SerializeField] private float _gamepadSensitivityMultiplier = 1f;
    private float _horizontalGamepadSensitivity, _verticalGamepadSensitivity;

    private float _curX, _curY;
    [SerializeField] private float _minY = -90f, _maxY = 90f;

    private bool _gamepad;
    private Vector2 _curInputs;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;

        _horizontalGamepadSensitivity = _horizontalSensitivity * _gamepadSensitivityMultiplier;
        _verticalGamepadSensitivity = _verticalSensitivity * _gamepadSensitivityMultiplier;
    }

    private void LateUpdate()
    {
        if(_gamepad)
        {
            RotateCamera(_curInputs, _horizontalGamepadSensitivity, _verticalGamepadSensitivity);
        }

        _cameraTransform.transform.position = _cameraFollowTransform.position;
    }

    public void OnCameraMoveMouse(InputAction.CallbackContext context)
    {
        _gamepad = false;
        RotateCamera(context.ReadValue<Vector2>(), _horizontalSensitivity, _verticalSensitivity);
    }

    public void OnCameraMoveGamepad(InputAction.CallbackContext context)
    {
        _gamepad = true;
        _curInputs = context.ReadValue<Vector2>();
    }

    private void RotateCamera(Vector2 inputs, float horizontalSensivity, float verticalSensivity)
    {
        _curX += inputs.x * horizontalSensivity;
        _curY = Mathf.Clamp(_curY + inputs.y * verticalSensivity, _minY, _maxY);

        _cameraTransform.rotation = Quaternion.Euler(-_curY, _curX, 0f);
    }

    private void OnValidate()
    {
        _horizontalGamepadSensitivity = _horizontalSensitivity * _gamepadSensitivityMultiplier;
        _verticalGamepadSensitivity = _verticalSensitivity * _gamepadSensitivityMultiplier;
    }
}
