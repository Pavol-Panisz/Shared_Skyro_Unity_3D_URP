using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AbilityBase fireball;
    public AbilityBase heal;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireball.TryUse();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            heal.TryUse();
        }
    }
}