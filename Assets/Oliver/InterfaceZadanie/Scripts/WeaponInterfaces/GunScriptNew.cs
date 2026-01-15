using UnityEngine;

namespace InterfaceZadanie
{
    public class GunScriptNew : MonoBehaviour, WeaponInterface
    {
        void WeaponInterface.PrimaryAction()
        {
            Debug.Log("Shoot");
        }

        void WeaponInterface.SecondaryAction()
        {
            Debug.Log("Reload");
        }
    }
}
