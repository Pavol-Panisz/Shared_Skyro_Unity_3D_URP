using UnityEngine;

public class RangeEnemy : BaseEnemyClass
{
    [SerializeField]private GameObject ammoPrefab;
    [SerializeField]private float shootForce;

    public override void Attack()
    {
        GameObject spawnedAmmo = Instantiate(ammoPrefab, transform.position + transform.forward, Quaternion.identity);
        spawnedAmmo.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        Destroy(spawnedAmmo, 5f);

        base.Attack();
    }

    public override void ControllEnemy()
    {
        switch (enemyState)
        {
            case EnemyState.Chase:
                agent.SetDestination(player.position);
            break;
            case EnemyState.Attack:
                agent.SetDestination(transform.position);
                if (canAttack) Attack();
                transform.LookAt(player);
            break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward, 0.1f);
    }
}
