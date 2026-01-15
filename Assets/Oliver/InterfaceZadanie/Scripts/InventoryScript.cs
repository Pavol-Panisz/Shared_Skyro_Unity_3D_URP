using System;
using System.Collections.Generic;
using UnityEngine;

namespace InterfaceZadanie
{
    public class InventoryScript : MonoBehaviour
    {
        [SerializeField]private List<GameObject> weapons;
        [SerializeField]private int selectedWeaponIndex;

        private void Update() {
            if (Input.GetMouseButtonDown(0))
            {
                DoAction(ActionType.PrimaryAction);
            }
            if (Input.GetMouseButtonDown(1))
            {
                DoAction(ActionType.SecondaryAction);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchWeapon();
            }
        }

        private void SwitchWeapon()
        {
            selectedWeaponIndex++;

            if (selectedWeaponIndex > weapons.Count - 1) selectedWeaponIndex = 0;
            
            Debug.Log("Selected weapon is " + weapons[selectedWeaponIndex]);
        }

        private void DoAction(ActionType actionType)
        {
            try
            {
                switch (actionType)
                {
                    case ActionType.PrimaryAction:
                        weapons[selectedWeaponIndex].GetComponent<WeaponInterface>().PrimaryAction();
                    break;
                    case ActionType.SecondaryAction:
                        weapons[selectedWeaponIndex].GetComponent<WeaponInterface>().SecondaryAction();
                    break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Weapon object {weapons[selectedWeaponIndex]} does not have Weapon interface" + "\n" + "\n" + e);
            }
        }
    }

    enum ActionType
    {
        PrimaryAction,
        SecondaryAction
    }
}
