using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem_ItemInputs : MonoBehaviour
{
    protected WeaponSystem_Item _item;

    [SerializeField] protected InputAction _useInputAction;

    protected virtual void Awake()
    {
        _item = GetComponent<WeaponSystem_Item>();

        _useInputAction.started += _item.OnUse;
        _useInputAction.canceled += _item.OnUse;
    }
}
