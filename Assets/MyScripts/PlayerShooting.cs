using Unity.Mathematics;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int bulletsCount = 10; // premenna s datovym typom int

    public GameObject bullet; // ten bullet ktory spawnujeme

    void Start()
    {
        Debug.Log("Hello world");
    }

    void Update()
    {
        Debug.Log("Momentalne mas " + bulletsCount + " bulletov");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bulletsCount < 0)
            {
                Debug.Log("kamo uz nemas bullety");
            }
            else
            {
                bulletsCount = bulletsCount - 1;

                Vector3 spawnPositon = transform.position + transform.forward;

                Instantiate(bullet, spawnPositon, transform.rotation);                
            }

        }
    }
}
