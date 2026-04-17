using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : Character
{
    public float attackRadius;


    private void Update()
    {
        if (Vector3.Distance(PlayerP.instance.transform.position, gameObject.transform.position) < attackRadius)
        {
            Attack();
        }
        else
        {
            Chase();
        }
    }
    public void Chase()
    {
        transform.LookAt(PlayerP.instance.transform.position);
        transform.position += Vector3.forward * walkSpeed * Time.deltaTime;
    }

    public override void DealDamage(int damage)
    {
        currHealthPoint -= damage;
        if (currHealthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}