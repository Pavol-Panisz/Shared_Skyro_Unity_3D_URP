using UnityEngine;

public class spinningball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().rotation = new (0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
