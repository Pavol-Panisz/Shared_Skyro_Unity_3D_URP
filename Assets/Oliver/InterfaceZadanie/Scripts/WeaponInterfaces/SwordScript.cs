using UnityEngine;
    
namespace InterfaceZadanie
{
    public class SwordScript : MonoBehaviour, WeaponInterface
    {
        void WeaponInterface.PrimaryAction()
        {
            Debug.Log("Slice");
        }

        void WeaponInterface.SecondaryAction()
        {
            
        }
    }
}
