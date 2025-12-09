using Unity.Mathematics;
using UnityEngine;

public class ShootingDemoPrvakom : MonoBehaviour
{
    public int bulletsCount = 10;
    public GameObject bulletPrefab;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.E))
        {
            if (bulletsCount > 0)
            {
                bulletsCount -= 1;  

                Vector3 spawnPosition =
                    transform.position + transform.forward;

                Instantiate(
                    bulletPrefab, 
                    spawnPosition,
                    transform.rotation
                );              
            }
        }

        Debug.Log("Bullets count: " + bulletsCount);
    }

}
