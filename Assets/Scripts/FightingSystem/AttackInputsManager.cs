using UnityEngine;
using UnityEngine.InputSystem;

public class AttackInputsManager : MonoBehaviour
{
    private AttackScript _attackScript;
    private InputAction _inputAction;

    private void Awake()
    {
        _attackScript = GetComponent<AttackScript>();
        if(_attackScript == null)
        {
            Destroy(this);
        }

        _inputAction = _attackScript.attackScriptableObject.attackInputAction;
    }

    private void OnEnable()
    {
        if (_inputAction == null) return;
        _inputAction.started += OnAttack;
        _inputAction.canceled += OnAttack;

        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.started -= OnAttack;
        _inputAction.canceled -= OnAttack;

        _inputAction.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        _attackScript.OnAttack(!context.canceled);
    }
}
