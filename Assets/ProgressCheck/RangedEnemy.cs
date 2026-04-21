using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : BaseEnemy, IDamageable
{
    public int projectileSpeed;
    public GameObject projectile;


    public override IEnumerator Attack()
    {
        while(gameObject != null)
        {
            var createdProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
            Vector2 directionToPlayer = (PlayerP.instance.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            createdProjectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            while (createdProjectile != null)
            {
                createdProjectile.transform.position += createdProjectile.transform.right * projectileSpeed * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
