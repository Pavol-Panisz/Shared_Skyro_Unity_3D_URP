using UnityEngine;

public class MirkovaHra_GameUIHandler : MonoBehaviour
{
    public static MirkovaHra_GameUIHandler Instance;

    [Header("Inventory")]
    [SerializeField] private GameObject _inventoryPanel;
    public GameObject inventoryPanel { get { return _inventoryPanel; } }
    [SerializeField] private Transform _inventoryContainer;
    public Transform inventoryContainer { get { return _inventoryContainer; } }
    [Header("Building")]
    [SerializeField] private GameObject _buildingPanel;
    public GameObject buildingPanel { get { return _buildingPanel; } }
    [SerializeField] private Transform _buildingsContainer;
    public Transform buildingContainer { get { return _buildingsContainer; } }

    private void Awake()
    {
        Instance = this;
    }
}
