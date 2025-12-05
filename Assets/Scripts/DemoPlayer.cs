using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    public DifficultySetting difficulty;

    int myHealth;

    void Start()
    {
        myHealth = difficulty.maxPlayerHealth;
    }
}
