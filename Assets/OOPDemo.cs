using UnityEngine;


public class BaseEnemy
{
    public int health;
    public string name;
    public float stopDistance, attackFrequency;

    public void GoToPosition(Vector3 vec)
    {
    }

    public virtual void Attack()
    {

    } // virtual - mozem overridnut no nemusim

}


public class RangedEnemy : BaseEnemy
{

}


public class OOPDemo : MonoBehaviour, IDamageable, IHealth
{
    public void TakeDamage(int amount)
    {

    }

    public void MakeInvincible(int duration)
    {

    }
}
