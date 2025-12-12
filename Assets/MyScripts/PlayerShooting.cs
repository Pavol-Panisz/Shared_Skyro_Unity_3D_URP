using UnityEngine;


public class PlayerShooting : MonoBehaviour

{
    public int bulletsCount = 10;
    public GameObject bullet;

    void Start()
    {
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bulletsCount < 0)
            {
            }
            else
            {
                bulletsCount = bulletsCount - 1;

                Vector3 spawnPositrion = transform.position + transform.forward * 1.0f;

                Instantiate(bullet, spawnPositrion, transform.rotation);
            }
        }
    }
}
