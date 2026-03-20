using UnityEngine;

public class MathExercise : MonoBehaviour
{
    public int bouncesCount = 0 ;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {

        bouncesCount++;

        if (bouncesCount % 3 == 0 & bouncesCount % 5 == 0)

        {
            Debug.Log("FizzBuzz");
        }
        else if (bouncesCount % 3 == 0)

        {
            Debug.Log("Fizz");
        }
        else if (bouncesCount % 5 == 0)
        {
            Debug.Log("Buzz");
        }
        else
        {
            Debug.Log(bouncesCount);
        }
           


    }




    void Update()
    {
        


    }
}
