using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManager needs that

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        if(gameObject.name != "Player") //need to change this from name to tag
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Player died, restarting level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //https://discussions.unity.com/t/level-restart/707461
        }
    }
}
