using System.Collections;
using UnityEngine;

public class MeleeEnemy : BaseEnemy, IDamageable
{
    public GameObject sword;
    public Transform swordPivot;


    public override IEnumerator Attack()
    {
        Vector3 playerPos = PlayerP.instance.transform.position;
        Vector3 dirToPlayer = playerPos - transform.position;
        dirToPlayer.z = 0;
        swordPivot.up = dirToPlayer;

        sword.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sword.SetActive(false);
    }
}