using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject ObjectToSpawn;

    void Start()
    {
        Vector3 spawnPosition = new Vector3(1.0f, 2.0f, 3.0f);
        Debug.Log("Poloha: " + spawnPosition.ToString());

        Instantiate(ObjectToSpawn, spawnPosition, transform.rotation);
    }
}
