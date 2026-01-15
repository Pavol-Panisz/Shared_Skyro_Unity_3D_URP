using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class HeldItem
{
    public virtual void PrimaryAction()
    {
        Debug.Log("Primary");
    }   
    public void SecondaryAction()
    {
        
    }
}

public class Sword : HeldItem
{
    public override void PrimaryAction()
    {
        Debug.Log("I sliced");
    }
}

public class Gun : HeldItem
{
    public override void PrimaryAction()
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
    
    public HeldItem currentItem = new Sword();
    
    
    private void Update()
    {

        // toggling between weapon
        if (WeaponEquipped == 0 && Input.GetKeyDown(KeyCode.E))
        {
           WeaponEquipped = 1;
        }
        else if(WeaponEquipped == 1 && Input.GetKeyDown(KeyCode.E))
        {
           WeaponEquipped = 0;
        }

        if (Input.GetMouseButtonDown(0)) // primary (left)
        {
            currentItem.PrimaryAction();
        }
        if (Input.GetMouseButtonDown(1)) // secondary (right)
        {
            currentItem.SecondaryAction();
        }
    }
}