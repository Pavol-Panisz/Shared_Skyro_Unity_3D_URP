using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int bulletCount = 10;
    public GameObject bullet;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Momentalne mas" + bulletCount + "bulletov");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bulletCount < 0)
            {
                Debug.Log("uz nemas bullet gadzo");
            }
            else
            {
               
                bulletCount = bulletCount - 1;

                Vector3 spawnPosition = transform.position + transform.forward * 2 ;

                Instantiate(bullet, spawnPosition, transform.rotation);
            }

            if(bulletCount <5)
            {
                Debug.Log("you need more bullets");
            }
        }
    }
}
