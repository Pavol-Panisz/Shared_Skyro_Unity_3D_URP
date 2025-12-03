using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MirkovaHra_BuildingSystem : MonoBehaviour
{
    public static MirkovaHra_BuildingSystem Instance;

    [SerializeField] private MirkovaHra_BuildingsList _buildingsList;
    private MirkovaHra_BuildingScriptableObject _curBuilding;
    public MirkovaHra_BuildingScriptableObject curBuilding
    {
        get { return _curBuilding; }
        set { _curBuilding = value; if (value == null) { _canBuild = false; } }
    }

    private Vector3 _curBuildPosition;
    private Vector3 _curBuildUpwardVector;
    private Transform _curBuildingCheckObject;
    private bool _canBuild;

    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _buildingsLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach(var building in _buildingsList.buildings)
        {
            MirkovaHra_BuildingUIHandler buildingUIHandler = Instantiate(Resources.Load<GameObject>("BuildingUI")).GetComponent<MirkovaHra_BuildingUIHandler>();
            buildingUIHandler.transform.parent = MirkovaHra_GameUIHandler.Instance.buildingContainer;
            buildingUIHandler.buildingScriptableObject = building;
            /*buildingUIHandler.sprite = building.buildingSprite;
            buildingUIHandler.name = building.name;
            buildingUIHandler.description = building.description;
            buildingUIHandler.neededResources = building.neededResources;
            buildingUIHandler.gettingResources = building.gettingResources;*/
        }
    }

    private void Update()
    {
        _canBuild = CanBuild();
    }

    private bool CanBuild()
    {
        if(_curBuilding == null) return false;
        RaycastHit raycastHit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 999f, _groundLayerMask))
        {
            if (Physics.CheckBox(raycastHit.point + raycastHit.normal * _curBuilding._buildingCheckSize.y, _curBuilding._buildingCheckSize / 2f, Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.forward, raycastHit.normal)), _buildingsLayerMask)) return false;
            //_curBuildingCheckObject.transform.position = raycastHit.point;

            _curBuildPosition = raycastHit.point;
            _curBuildUpwardVector = raycastHit.normal;
            
            return true;
        }
        return false;
    }

    public void OnBuild(InputAction.CallbackContext context)
    {
        if (!context.started || EventSystem.current.IsPointerOverGameObject()) return;

        Build();
    }

    private void Build()
    {
        if (!_canBuild) return;
        if(curBuilding.neededResources.Count == 0 || MirkovaHra_InventorySystem.Instance.RemoveItems(GetDictionaryFromBuildResources(curBuilding.neededResources)))
        {
            MirkovaHra_Building building = CreateBuilding(curBuilding, _curBuildPosition, _curBuildUpwardVector);
        }
    }

    private MirkovaHra_Building CreateBuilding(MirkovaHra_BuildingScriptableObject buildingScriptableObject, Vector3 position, Vector3 upwardVector)
    {
        MirkovaHra_Building building = Instantiate(buildingScriptableObject.buildingPrefab).GetComponent<MirkovaHra_Building>();
        building.transform.position = position;
        building.transform.up = upwardVector;

        return null;
    }

    private Dictionary<MirkovaHra_ItemScriptableObject, int> GetDictionaryFromBuildResources(List<MirkovaHra_BuildResources> resources)
    {
        Dictionary<MirkovaHra_ItemScriptableObject, int> dictionary = new Dictionary<MirkovaHra_ItemScriptableObject, int>();
        foreach (var item in resources)
        {
            if (!dictionary.ContainsKey(item.item))
            {
                dictionary.Add(item.item, item.count);
            }
            else
            {
                dictionary[item.item] += item.count;
            }
        }
        
        return dictionary;
    }

    public void SelectCurrentBuilding(MirkovaHra_BuildingScriptableObject building)
    {
        curBuilding = building;
    }
}
