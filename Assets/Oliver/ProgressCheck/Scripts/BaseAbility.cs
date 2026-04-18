using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    public string key;
    public float cooldown;
    public bool canUseAbility;

    public abstract void ActivateAbility();
    public virtual void ResetAbility()
    {
        canUseAbility = true;
    }
}
