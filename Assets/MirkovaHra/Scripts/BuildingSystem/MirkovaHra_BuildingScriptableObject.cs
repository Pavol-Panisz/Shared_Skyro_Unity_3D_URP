using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBuilding", menuName = "Mirkova Hra/Building System/Create new Building")]
public class MirkovaHra_BuildingScriptableObject : ScriptableObject
{
    public string name;
    public string description;
    public Sprite buildingSprite;
    public List<MirkovaHra_BuildResources> neededResources = new List<MirkovaHra_BuildResources>();
    public List<MirkovaHra_BuildResources> gettingResources = new List<MirkovaHra_BuildResources>();
    public float gettingResourcesCooldown;
    public Vector3 _buildingCheckSize;
    public GameObject buildingPrefab;
}

[Serializable]
public class MirkovaHra_BuildResources
{
    public MirkovaHra_ItemScriptableObject item;
    public int count;
}