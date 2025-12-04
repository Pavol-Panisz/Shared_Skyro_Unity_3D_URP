using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleGunScript : GunScript
{


    public override void StartAttack(InputAction.CallbackContext context)
    {
        base.StartAttack(context);

        Fire();
    }

    public override void Fire()
    {
        base.Fire();
    }

    public override void Reload()
    {
        base.Reload();
    }
}
