using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicSpawner : MonoBehaviour
{
    public GameObject prefabtospawn;
    public int numbertospawn;
    private int coinsspawned = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            coinsspawned = coinsspawned + 1;
            if (coinsspawned < numbertospawn)
            {
                 
                {
                    
                    coinsspawned = coinsspawned + 1;
                    Instantiate(prefabtospawn, transform.position, transform.rotation);
                  
                }
                
            }

        }
        }
    }

