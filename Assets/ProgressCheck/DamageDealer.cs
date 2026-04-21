using System.Collections;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;
    public bool destroyOnHit = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hit = collision.gameObject;

        GameObject owner = GetRootOwner();

        if (hit == owner || hit.transform.IsChildOf(owner.transform)) return;

        var damageable = hit.GetComponent<IDamageable>();
        if (damageable == null) return;

        damageable.DealDamage(damage);

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
    private GameObject GetRootOwner()
    {
        Transform current = transform.parent;
        while (current != null)
        {
            if (current.GetComponent<IDamageable>() != null)
                return current.gameObject;
            current = current.parent;
        }
        return gameObject;
    }
}