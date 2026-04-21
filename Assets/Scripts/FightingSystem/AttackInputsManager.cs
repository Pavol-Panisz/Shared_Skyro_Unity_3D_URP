using UnityEngine;
using UnityEngine.InputSystem;

public class AttackInputsManager : MonoBehaviour
{
    private AttackScript _attackScript;

    private void Awake()
    {
        _attackScript = GetComponent<AttackScript>();
        if(_attackScript == null)
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        _attackScript.attackScriptableObject.attackInputAction.started += _attackScript.OnAttack;
        _attackScript.attackScriptableObject.attackInputAction.canceled += _attackScript.OnAttack;
    }
}
