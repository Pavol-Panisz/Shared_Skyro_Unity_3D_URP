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

        if (gameObject.CompareTag("Enemy"))  //using tag instead of name
                                             //https://docs.unity3d.com/ScriptReference/GameObject.CompareTag.html
        {
            StartCoroutine(EnemyDeath());
        }
        else if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Player died, restarting level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //https://discussions.unity.com/t/level-restart/707461
        }
    }

    IEnumerator EnemyDeath()
    {
        //disable the Enemy script so it stops moving/shooting
        Enemy enemyScript = GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.enabled = false;
        }

        //make it fall (disable kinematic if it has a rigidbody)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        if (rb != null)
        {
            rb.AddTorque(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f), ForceMode.Impulse);
        }

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
