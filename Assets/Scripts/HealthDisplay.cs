using UnityEngine.UI;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public SOFloatVar healthVar;
    public Image image;
    

    
    void Update()
    {
        image.fillAmount = healthVar.value / 100f;
    }
}

