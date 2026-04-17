using System.Collections;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public int projectileSpeed;
    public GameObject projectile;
    public override IEnumerator Attack()
    {
        while(gameObject != null)
        {
            var createdProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            while (createdProjectile != null)
            {
                createdProjectile.transform.position += Vector3.forward * projectileSpeed * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
