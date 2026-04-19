using UnityEngine;

public class FireballAbility : BaseAbility
{
    [SerializeField]private GameObject fireballPrefab;
    private Transform player;

    void Start()
    {
        player = FindAnyObjectByType<Player>().transform;
    }

    public override void ActivateAbility()
    {
        SpawnFireball();
    }

    private void SpawnFireball()
    {
        GameObject spawnedFireball = Instantiate(fireballPrefab, player.position, Quaternion.identity, null);

        spawnedFireball.GetComponent<Rigidbody>().AddForce(player.transform.forward * 500);
    }
}
