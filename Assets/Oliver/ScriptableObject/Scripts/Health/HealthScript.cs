using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]private SOFloatVar healthSO;

    public float GetHealth()
    {
        return healthSO.value;
    }
}
