using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : Character
{
    public float attackRadius;


    private void Awake()
    {
        StartCoroutine(Decide());
    }

    IEnumerator Decide()
    {
        while(gameObject != null)
        {
            print(Vector3.Distance(PlayerP.instance.transform.position, gameObject.transform.position));
            if (Vector3.Distance(PlayerP.instance.transform.position, gameObject.transform.position) < attackRadius)
            {
                StartCoroutine(Attack());
                yield return new WaitForSeconds(attackSpeed);
            }
            else
            {
                Chase();
                yield return null;
            }
        }
    }
    public void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerP.instance.transform.position, walkSpeed * Time.deltaTime);
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