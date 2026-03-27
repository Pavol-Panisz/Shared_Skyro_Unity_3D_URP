using UnityEngine;

public class HealAbility : AbilityBase
{
    public float healAmount = 25f;

    protected override void Use()
    {
        GetComponent<HealthComponent>().Heal(healAmount);
    }
}