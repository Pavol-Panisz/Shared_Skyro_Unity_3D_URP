using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    public PlayerController player;
    private Label speedLabel;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        speedLabel = root.Q<Label>("speed-label");
    }

    void Update()
    {
        if (player != null && speedLabel != null)
        {
            speedLabel.text = $"Speed: {player.CurrentSpeed:F1} m/s";
        }
    }
}