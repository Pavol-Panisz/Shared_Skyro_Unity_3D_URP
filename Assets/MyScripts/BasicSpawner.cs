using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn ;
    public int prefabCount = 10; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (prefabCount <0)
            {
                Debug.Log("Prekrocil si limit bro");
            }
            else
            {
                Debug.Log("spawnuty objekt " + prefabCount + " z maximalneho poctu 10");
                
                prefabCount = prefabCount - 1;
                Vector3 spawnPosition = transform.position + transform.forward * 2;

                Instantiate(prefabToSpawn, spawnPosition, transform.rotation);

               
            }
          
          









        }
    }
}
