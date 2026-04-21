using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    [HideInInspector] public HealthSystem attacker;
    private AttackScriptableObject _attackScriptableObject;
    public virtual AttackScriptableObject attackScriptableObject { get { return _attackScriptableObject; } set { _attackScriptableObject = value; _curAttackCooldown = value.attackCooldown; } }

    private float _curAttackCooldown;
    private bool _holdingAttackButton;

    private void Update()
    {
        AttackCooldown();
    }

    public bool CanAttack()
    {
        return _curAttackCooldown >= attackScriptableObject.attackCooldown;
    }

    public virtual void Attack()
    {
        if (!CanAttack()) return;

        _curAttackCooldown = 0f;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        print(context.started);
        OnAttack(!context.canceled);
    }

    public void OnAttack(bool attacking)
    {
        _holdingAttackButton = attacking;
        if (attacking)
        {
            Attack();
        }
    }

    private void AttackCooldown()
    {
        if (_curAttackCooldown < attackScriptableObject.attackCooldown)
        {
            _curAttackCooldown += Time.deltaTime;
            if (_curAttackCooldown >= attackScriptableObject.attackCooldown)
            {
                OnAttackCooldown();
            }
        }
    }

    private void OnAttackCooldown()
    {
        if(_holdingAttackButton)
        {
            Attack();
        }
    }
}
