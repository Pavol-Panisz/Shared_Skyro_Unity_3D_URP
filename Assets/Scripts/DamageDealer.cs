using System;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool destroyAfterwards;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var damageable = other.gameObject.GetComponentInChildren<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage(damage);
            
            if(destroyAfterwards)
                Destroy(gameObject);
        }
    }
}