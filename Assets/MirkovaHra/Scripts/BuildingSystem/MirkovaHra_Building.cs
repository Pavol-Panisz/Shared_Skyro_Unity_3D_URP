using System.Collections.Generic;
using UnityEngine;

public class MirkovaHra_Building : MonoBehaviour
{
    [SerializeField] private MirkovaHra_BuildingScriptableObject _scriptableObject;

    private float _gettingResourceCooldown;
    private float _curGettingResourceCooldown;

    private List<MirkovaHra_BuildResources> _gettingResources = new List<MirkovaHra_BuildResources>();

    private void Awake()
    {
        if(!_scriptableObject) enabled = false;

        _gettingResourceCooldown = _scriptableObject.gettingResourcesCooldown;
        _curGettingResourceCooldown = 0f;

        _gettingResources = _scriptableObject.gettingResources;
    }

    private void Update()
    {
        _curGettingResourceCooldown += Time.deltaTime;
        if (_curGettingResourceCooldown >= _gettingResourceCooldown)
        {
            _curGettingResourceCooldown = 0f;

            foreach(var resource in _gettingResources)
            {
                MirkovaHra_InventorySystem.Instance.AddItems(resource.item, resource.count);
            }
        }
    }
}
