using UnityEngine;
using UnityEngine.UI;

public class HealthGUIDisplay : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] Slider healthSlider;
    public SOSO SO_Health;

    private void Update()
    {
        healthSlider.value = SO_Health.health / 100f;
    }

}