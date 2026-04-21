using System.Collections;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public RangedEnemy rangedEnemy;
    public MeleeEnemy meleeEnemy;

    private void Awake()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            var number = Random.Range(0, 2);

            if (number == 0)
            {
                Instantiate(rangedEnemy, new Vector3(-5, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(meleeEnemy, new Vector3(5, 0, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(4, 9));
        }
    }
}