using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    public int bulletsCount = 10;


    void Start()
    {
        Debug.Log("Hello world");
    }


    void Update()
    {   
        Debug.Log("Bullets left: " + bulletsCount);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bulletsCount < 0)
            {
                Debug.Log("Nemas pew pew!");
            }
            else
            {
                bulletsCount = bulletsCount - 1;
                instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                Debug.Log("Pew pew!");
            }
        }
    }
}
