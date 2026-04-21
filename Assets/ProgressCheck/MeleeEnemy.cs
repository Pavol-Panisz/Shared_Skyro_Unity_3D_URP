using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : BaseEnemy, IDamageable
{
    public GameObject sword;
    public Transform swordPivot;


    public override IEnumerator Attack()
    {
        while (Vector3.Distance(PlayerP.instance.transform.position, transform.position) < attackRadius)
        {
            swordPivot.LookAt(PlayerP.instance.transform.position);
            sword.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            sword.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            yield return null;
        }
    }
}
