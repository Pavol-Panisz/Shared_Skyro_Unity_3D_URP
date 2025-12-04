using UnityEngine;

[CreateAssetMenu(fileName = "BasicGunDataSOScript", menuName = "Scriptable Objects/BasicGunDataSOScript")]
public class BasicGunDataSOScript : ScriptableObject
{
    public int maxAmmoInMagazine;
    public float fireRate;
    public int reloadTime;

    [Header("Physics")]
    public float shootForce;

    public GameObject ammoPrefab;
}
