using UnityEngine;
using UnityEngine.Rendering;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    int currentAmmo = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true && currentAmmo >= 1)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            currentAmmo --;
        }
    }

    public void AddAmmo(int ammo)
    {
        currentAmmo = currentAmmo + ammo;
    }

    public void SerAmmo(int ammo)
    {
        currentAmmo = ammo;
    }
}
