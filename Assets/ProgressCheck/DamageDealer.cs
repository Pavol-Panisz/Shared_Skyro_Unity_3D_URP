using System.Collections;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        var objectToDamage = collision.gameObject.GetComponent<IDamageable>();
        objectToDamage.DealDamage(damage);
        if (!gameObject.GetComponent<DamageAbility>() && gameObject != PlayerP.instance.sword)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.1f);
        if(gameObject != null) { Destroy(gameObject); }
    }
}