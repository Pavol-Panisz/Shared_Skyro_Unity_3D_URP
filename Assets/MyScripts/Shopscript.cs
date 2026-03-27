using UnityEngine;

public class Shopscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int price = 10;
    public int money = 100;


    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void neviem ()
    {
        money -= price;
    }


}
