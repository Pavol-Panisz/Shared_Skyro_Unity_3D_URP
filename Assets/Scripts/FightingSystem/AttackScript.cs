using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AttackScript : MonoBehaviour
{
    [HideInInspector] public HealthSystem attacker;
    private AttackScriptableObject _attackScriptableObject;
    public virtual AttackScriptableObject attackScriptableObject { get { return _attackScriptableObject; } set { _attackScriptableObject = value; _curAttackCooldown = value.attackCooldown; } }

    private float curattackcooldown;
    protected float _curAttackCooldown {
        get { return curattackcooldown; }
        set
        {
            curattackcooldown = value;
            if(attackCooldownUI != null)
            {
                attackCooldownUI.fillAmount = value / attackScriptableObject.attackCooldown;
            }
        }
    }
    private bool _holdingAttackButton;

    [HideInInspector] public Image attackCooldownUI;

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
        _curAttackCooldown = 0f;
    }

    public void OnAttack(bool attacking)
    {
        print(attacking);
        _holdingAttackButton = attacking;
        if (attacking && CanAttack())
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
        if (_holdingAttackButton && CanAttack())
        {
            Attack();
        }
    }

    private void OnDestroy()
    {
        if(attackCooldownUI != null)
            Destroy(attackCooldownUI.gameObject);
    }
}
