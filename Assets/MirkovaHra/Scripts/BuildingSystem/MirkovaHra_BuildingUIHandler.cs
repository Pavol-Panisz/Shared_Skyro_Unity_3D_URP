using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MirkovaHra_BuildingUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    private List<MirkovaHra_ResourceUIHandler> _neededResourceUIs;
    private List<MirkovaHra_ResourceUIHandler> _gettingResourcesUIs;
    [SerializeField] private Transform _neededResourcesContainer;
    [SerializeField] private Transform _gettingResourcesContainer;
    [SerializeField] private Image _image;

    private MirkovaHra_BuildingScriptableObject _buildingScriptableObject;
    public MirkovaHra_BuildingScriptableObject buildingScriptableObject
    {
        get { return _buildingScriptableObject; }
        set
        {
            _buildingScriptableObject = value;
            name = value.name;
            description = value.description;
            sprite = value.buildingSprite;
            neededResources = value.neededResources;
            gettingResources = value.gettingResources;
            GetComponent<Button>().onClick.AddListener(() => { MirkovaHra_BuildingSystem.Instance.SelectCurrentBuilding(value); });
        }
    }
    private string _name;
    public string name { get { return _name; } set { _name = value; _nameText.text = value; } }
    private string _description;
    public string description { get { return _description; } set { _description = value; _descriptionText.text = value; } }
    private Sprite _sprite;
    public Sprite sprite { get { return _sprite; } set { _sprite = value; _image.sprite = value; } }
    private List<MirkovaHra_BuildResources> _neededResources;
    public List<MirkovaHra_BuildResources> neededResources
    {
        get { return _neededResources; }
        set
        {
            _neededResources = value; _neededResourceUIs = CreateNewResourceUIHandlers(value);
            foreach (var uiHandler in _neededResourceUIs)
            {
                uiHandler.transform.SetParent(_neededResourcesContainer);
            }
        }
    }
    private List<MirkovaHra_BuildResources> _gettingResources;
    public List<MirkovaHra_BuildResources> gettingResources
    {
        get { return _gettingResources; }
        set
        {
            _gettingResources = value; _gettingResourcesUIs = CreateNewResourceUIHandlers(value);
            foreach (var uiHandler in _gettingResourcesUIs)
            {
                uiHandler.transform.SetParent(_gettingResourcesContainer);
            }
        }
    }

    private List<MirkovaHra_ResourceUIHandler> CreateNewResourceUIHandlers(List<MirkovaHra_BuildResources> resources)
    {
        List<MirkovaHra_ResourceUIHandler> resourcesUIHandlers = new List<MirkovaHra_ResourceUIHandler>();
        foreach (var resource in resources)
        {
            resourcesUIHandlers.Add(CreateNewResourceUIHandler(resource)); 
        }

        return resourcesUIHandlers;
    }

    private MirkovaHra_ResourceUIHandler CreateNewResourceUIHandler(MirkovaHra_BuildResources resource)
    {
        MirkovaHra_ResourceUIHandler resourceUIHandler = Instantiate(Resources.Load<GameObject>("ResourceUI")).GetComponent<MirkovaHra_ResourceUIHandler>();
        resourceUIHandler.sprite = resource.item.itemSprite;
        resourceUIHandler.count = resource.count;

        return resourceUIHandler;
    }
}
