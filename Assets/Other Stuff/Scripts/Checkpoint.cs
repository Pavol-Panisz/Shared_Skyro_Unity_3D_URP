using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public float spawnOffset = 3f;
    public UnityEvent OnThisSetAsRespawn;
    public UnityEvent OnOtherSetAsRespawn;

    Checkpoint[] allCheckpoints;

    private void Start()
    {
        allCheckpoints = Object.FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            KillPlayer.playerRespawnPoint = transform.position + Vector3.up * spawnOffset;

            foreach (var checkpoint in allCheckpoints)
            {
                if (checkpoint == this)
                    OnThisSetAsRespawn?.Invoke();
                else
                    checkpoint.OnOtherSetAsRespawn?.Invoke();
            }
        }
    }
}
