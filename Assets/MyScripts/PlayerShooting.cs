using UnityEngine;


public class PlayerShooting : MonoBehaviour

{
    public int bulletsCount = 10;
    public GameObject bullet;

    void Start()
    {
        Debug.Log("Hello world");
    }


    void Update()
    {
        Debug.Log("Momentalne mas: " + bulletsCount + " bulletov");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bulletsCount < 0)
            {
                Debug.Log("Nemas pew pew!");
            }
            else
            {
                bulletsCount = bulletsCount - 1;

                Vector3 spawnPositrion = transform.position + transform.forward * 1.0f;

                Instantiate(bullet, spawnPositrion, transform.rotation);

                Debug.Log("Pew pew!");
                Debug.Log("Bullets left: " + bulletsCount);
            }
        }
    }
}
