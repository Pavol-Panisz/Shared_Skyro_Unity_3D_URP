using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private HealthSystem _attacker;

    private float _damage;
    private float _lifeTime;

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

    private void OnTriggerEnter(Collider other)
    {
        if (IsThereAPlayer(other, out HealthSystem healthSystem, out bool isAttacker))
        {
            if (isAttacker) return;
            healthSystem.Damage(_damage);
        }
    }

    private bool IsThereAPlayer(Collider other, out HealthSystem healthSystem, out bool isAttacker)
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

    public Hitbox(Vector3 hitboxSize, Vector3 spawnPos, Vector3 forwardVector, float damage, float lifeTime, HealthSystem attacker)
    {
        GameObject hitboxObject = Instantiate(Resources.Load<GameObject>("Hitbox"), spawnPos, Quaternion.identity);
        
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
        }
    }

    public static Vector3 GetHitboxSpawnPosition(Vector3 size, Vector3 position, Vector3 forwardVector)
    {
        return position + forwardVector * size.z;
    }
}
