using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    public string key;
    public float cooldown;
    public bool canUseAbility;

    public abstract void ActivateAbility();

    void Update()
    {
        if (Input.GetKeyDown(key) && canUseAbility)
        {
            ActivateAbility();
            canUseAbility = false;
            Invoke(nameof(ResetAbility), cooldown);
        }
    }

    public virtual void ResetAbility()
    {
        canUseAbility = true;
    }
}
