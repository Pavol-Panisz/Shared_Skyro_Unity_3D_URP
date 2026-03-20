using UnityEngine;


public class OOPDemo : MonoBehaviour, IDamageable
{
    
    public int Health
    {
        get 
        {
            Debug.Log("someone ELSE is reading health");    
            return Health;
        }
        private set 
        {
            Health = value;

            if (Health > 100) Health = 100;

            if (Health < 0) Health = 0;
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
    }

}
