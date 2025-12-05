using System;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    public GameObject prefabtospawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E has been pressed!");
            Instantiate(prefabtospawn, transform.position, transform.rotation);
        }
        else         {
            Debug.Log("E has not been pressed!");
        }
    }
}
