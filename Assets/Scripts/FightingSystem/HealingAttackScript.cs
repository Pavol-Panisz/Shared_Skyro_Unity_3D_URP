using UnityEngine;

public class HealingAttackScript : AttackScript
{
    public override void Attack()
    {
        base.Attack();
        attacker.Damage(-attackScriptableObject.attackDamage);
    }
}
