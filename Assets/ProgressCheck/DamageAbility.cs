using System.Collections;
using UnityEngine;

public class DamageAbility : BaseAbility
{
    public GameObject projectile;
    public override IEnumerator AbilityActivate()
    {
        if (Input.GetKeyDown(KeyCode.E) && cooldown <= 0)
        {
            var fireball = Instantiate(projectile, transform.position, Quaternion.identity, transform);
            fireball.GetComponent<Rigidbody2D>().linearVelocity = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return new WaitForSeconds(cooldown);
        }
    }
}