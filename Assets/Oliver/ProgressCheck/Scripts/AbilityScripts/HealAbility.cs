using UnityEngine;

public class HealAbility : BaseAbility
{
    [SerializeField]private int healAmount;
    private Player player;

    private void Start() {
        player = FindAnyObjectByType<Player>();
    }

    public override void ActivateAbility()
    {
        player.Heal(healAmount);
    }
}
