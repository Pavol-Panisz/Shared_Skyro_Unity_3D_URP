using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    void Start()
    {
        Vector3 myVector = new Vector3(1.0f, 2.0f, 3.0f);
        Debug.Log("Poloha: " + myVector.ToString());
    }

    void Update()
    {
        
    }
}
