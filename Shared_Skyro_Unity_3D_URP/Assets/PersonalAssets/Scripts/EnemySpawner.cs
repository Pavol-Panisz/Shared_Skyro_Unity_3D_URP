using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnEnemy()); //https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MonoBehaviour.StartCoroutine.html
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(enemyPrefab, new Vector3(0f,0f,0f), Quaternion.identity);   //https://discussions.unity.com/t/how-to-spawn-a-prefab-from-the-script-c/943047
                                                                                    //https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
                                                                                    //https://discussions.unity.com/t/how-to-spawn-an-prefab/776669
                                                                                    //yes i found a lot of shit
        yield return new WaitForSeconds(spawnInterval);
        }
    }
}
