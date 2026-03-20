using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        // toto je zly priklad ale aspon tu vidno ako sa pouziva interface a setter

        OOPDemo demo = collision.gameObject.GetComponent<OOPDemo>();

        Debug.Log(demo.Health);

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        damageable.TakeDamage(5);
    }
}
