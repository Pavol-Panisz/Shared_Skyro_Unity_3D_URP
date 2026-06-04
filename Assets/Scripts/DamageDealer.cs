using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool destroyAfterwards;
    [SerializeField] private bool damageOnCollision;
    [SerializeField] private new Collider2D collider2D;
    [SerializeField] private ContactFilter2D filter2D;

    private void Update()
    {
        if(!damageOnCollision)
            return;

        DamageColliders();
    }
    
    public void ExternalAttack()
        => DamageColliders();

    private void DamageColliders()
    {
        List<Collider2D> colliders = new();
        collider2D.Overlap(filter2D, colliders);
        
        foreach (var col in colliders)
        {
            col.gameObject.GetComponentInChildren<IDamageable>()?.Damage(damage);
            
            if(!destroyAfterwards) continue;
            
            Destroy(gameObject);
            return;
        }
    }

}