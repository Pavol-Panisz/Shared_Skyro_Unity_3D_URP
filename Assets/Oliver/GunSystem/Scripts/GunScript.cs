using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    [Header("Gun Variables")]
    public int ammoLeftInMagazine;
    [HideInInspector]public bool canFire;

    [Header("Gun Settings")]
    public BasicGunDataSOScript basicGunDataSO;

    public virtual void StartAttack(InputAction.CallbackContext context)
    {
        if (ammoLeftInMagazine <= 0)
        {
            return;
        }
    }

    public virtual void Fire()
    {
        Invoke(nameof(ResetFire), basicGunDataSO.fireRate);
    }

    public virtual void Reload()
    {
        
    }

    public virtual void ResetFire()
    {
        
    }
}
