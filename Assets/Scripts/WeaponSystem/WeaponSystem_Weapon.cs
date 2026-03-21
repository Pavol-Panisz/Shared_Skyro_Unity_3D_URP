using UnityEngine;

public class WeaponSystem_Weapon : WeaponSystem_Item
{
    protected bool _attacking;

    protected override void Use()
    {
        base.Use();

        Attack();
    }

    protected virtual void Attack()
    {
        _attacking = true;
    }

    protected virtual void OnAttackEnded() // <-- Will be called by animation event
    {
        _attacking = false;
    }

    protected override bool CanUse()
    {
        return !_attacking && base.CanUse();
    }
}
