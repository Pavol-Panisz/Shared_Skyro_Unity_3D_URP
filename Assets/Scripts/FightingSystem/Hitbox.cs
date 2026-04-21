using UnityEngine;

public class Hitbox : MonoBehaviour
{
    protected HealthSystem _attacker;

    protected float _damage;
    protected float _lifeTime;

    private void Update()
    {
        HandleLifeTime();
    }

    private void HandleLifeTime()
    {
        _lifeTime -= Time.deltaTime;
        if(_lifeTime <= 0f)
        {
            DestroyHitbox();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (IsThereAPlayer(other, out HealthSystem healthSystem, out bool isAttacker))
        {
            if (isAttacker) return;
            healthSystem.Damage(_damage);
        }
    }

    protected bool IsThereAPlayer(Collider other, out HealthSystem healthSystem, out bool isAttacker)
    {
        isAttacker = false;
        if(other.TryGetComponent(out healthSystem))
        {
            if (healthSystem == _attacker)
                isAttacker = true;
            return true;
        }

        return false;
    }

    private void DestroyHitbox()
    {
        Destroy(gameObject);
    }

    public static Hitbox SpawnHitbox(Vector3 hitboxSize, Vector3 spawnPos, Vector3 forwardVector, float damage, float lifeTime, HealthSystem attacker)
    {
        GameObject hitboxObject = Instantiate(Resources.Load<GameObject>("Hitbox"));
        
        if(hitboxObject.TryGetComponent(out BoxCollider hitboxCollider))
        {
            hitboxCollider.size = hitboxSize;
        }

        hitboxObject.transform.position = spawnPos;
        hitboxObject.transform.forward = forwardVector;

        if (hitboxObject.TryGetComponent(out Hitbox hitboxScript))
        {
            hitboxScript._damage = damage;
            hitboxScript._lifeTime = lifeTime;
            hitboxScript._attacker = attacker;
        }

        return hitboxScript;
    }

    public static Vector3 GetHitboxSpawnPosition(Vector3 size, Vector3 position, Vector3 forwardVector)
    {
        return position + forwardVector * size.z;
    }
}
