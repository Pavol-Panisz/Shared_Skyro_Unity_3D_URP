using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem_Item : MonoBehaviour
{
    [SerializeField] protected float _useCooldown;
    protected float _curUseCooldown;

    [SerializeField] private bool _canHoldUseButton;

    protected bool _using;

    protected virtual void Update()
    {
        if(_curUseCooldown >= 0f)
        {
            _curUseCooldown -= Time.deltaTime;
            if(_canHoldUseButton && CanUse())
            {
                Use();
            }
        }
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        OnUse(!context.canceled);
    }

    protected virtual void OnUse(bool use)
    {
        _using = use;

        if(CanUse())
        {
            Use();
        }
    }

    protected virtual void Use()
    {
        _curUseCooldown = _useCooldown;
    }

    protected virtual bool CanUse()
    {
        return _using && _curUseCooldown <= 0f;
    }
}
