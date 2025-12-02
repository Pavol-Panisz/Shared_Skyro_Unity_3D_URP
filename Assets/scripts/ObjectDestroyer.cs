using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
