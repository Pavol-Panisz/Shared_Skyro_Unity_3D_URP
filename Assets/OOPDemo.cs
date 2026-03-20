
using UnityEngine;
using System.Collections.Generic;

public class Car
{
    public string brand;
    public float topSpeed;

    // default constructor
    public Car()
    {
        brand = "Toyota";
        topSpeed = 0;
    }

    // druhy constructor
    public Car(string brand, float topSpeed)
    {
        this.brand = brand;
        this.topSpeed = topSpeed;
    }

    ~Car()
    {
        Debug.Log("Goodbye cruel world");
    }
}
public class OOPDemo : MonoBehaviour 
{
    List<Car> cars;

    void Start()
    {
        cars = new List<Car>();

        Car car1 = new Car("VW", 180f);

        cars.Add(car1);

        cars.Clear();
    }


}