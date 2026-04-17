using System;
using UnityEngine;

public class HealAbility : BaseAbility
{
    [SerializeField, Min(0)] private int healAmount;
    private IDamageable _playerDamageableCache;

    private void Start()
    {
        _playerDamageableCache = Player.Instance.GetComponent<IDamageable>();
    }

    protected override void Activate()
    {
        _playerDamageableCache.Damage(-healAmount);
    }
}