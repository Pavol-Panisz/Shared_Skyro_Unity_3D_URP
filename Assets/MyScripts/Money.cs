using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //public int currentMoney = 0;

    // [Range(0, 1)]
    // public float discountPercentage;

    // public int itemPrice;

    // public int remainingMoney;

    public int maxAmount;
    public int currentNumber;
    public int multiplier ;

    void Start()
    {

    }



    // Update is called once per frame

    
    void Update()
    {
        //remainingMoney = currentMoney - (itemPrice - (int)(itemPrice * discountPercentage)); 
       if (maxAmount > currentNumber)
        {
            currentNumber++; 
        }
      
            
         
    }
}
