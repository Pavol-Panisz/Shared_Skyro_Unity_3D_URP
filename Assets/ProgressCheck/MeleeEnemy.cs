using System.Collections;
using UnityEngine;

public class MeleeEnemy : BaseEnemy, IDamageable
{
    public GameObject sword;
    public Transform swordPivot;


    public override IEnumerator Attack()
    {
        swordPivot.LookAt(PlayerP.instance.transform.position);

        sword.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sword.SetActive(false);
    }
}