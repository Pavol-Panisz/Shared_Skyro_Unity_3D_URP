using System.Collections.Generic;
using UnityEngine;

public class Sword
{
    public void Slice()
    {
        Debug.Log("I sliced");
    }
}

public class Gun
{
    public void Shoot()
    {
        Debug.Log("I shot");
    }
    public void Reload()
    {
        Debug.Log("I reloaded");
    }
}
public class Player : MonoBehaviour
{
    public int WeaponEquipped = 0;
    private Sword sword = new Sword();
    private Gun gun = new Gun();

    public int InventorySize = 5;
    private List<int> Hotbar = new List<int>();
    private int i = 0;

    private void Awake()
    {
        Hotbar.Capacity = InventorySize;
        WeaponEquipped = i;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            i++;
            WeaponEquipped = i;
            if (i > Hotbar.Count)
            {
                i = 0;
            }
        }

        //if (WeaponEquipped == 0 && Input.GetKeyDown(KeyCode.E))
        //{
        //    WeaponEquipped = 1;
        //}
        //else if(WeaponEquipped == 1 && Input.GetKeyDown(KeyCode.E)N
        //{
        //    WeaponEquipped = 0;
        //}

        if(WeaponEquipped == 0 && Input.GetMouseButtonDown(0))
        {
            sword.Slice();
        }
        else if(WeaponEquipped == 1 && Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }
        else if (WeaponEquipped == 1 && Input.GetMouseButtonDown(1))
        {
            gun.Reload();
        }
    }
}
