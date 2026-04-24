using UnityEngine;
using UnityEngine.UI;

public abstract class BaseAbility : MonoBehaviour
{
    public string key;
    public float cooldown;
    public bool canUseAbility;

    [SerializeField]private protected Image abilityImg;

    public abstract void ActivateAbility();

    void Update()
    {
        if (Input.GetKeyDown(key) && canUseAbility)
        {
            ActivateAbility();
            canUseAbility = false;
            Invoke(nameof(ResetAbility), cooldown);
            abilityImg.color = Color.gray;
        }
    }

    public virtual void ResetAbility()
    {
        canUseAbility = true;
        abilityImg.color = Color.white;
    }
}
