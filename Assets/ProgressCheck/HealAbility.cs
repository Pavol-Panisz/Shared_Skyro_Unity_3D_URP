using System.Collections;
using UnityEngine;

public class HealAbility : BaseAbility
{
    public override IEnumerator AbilityActivate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && cooldown == 0)
        {
            GetComponent<PlayerP>().currHealthPoint += 4;
            Mathf.Clamp(GetComponent<PlayerP>().currHealthPoint, 0, GetComponent<PlayerP>().maxHealthPoints);
            yield return new WaitForSeconds(cooldown);
        }
    }
}