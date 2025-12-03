using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBuildingsList", menuName = "Mirkova Hra/Building System/Create new Buildings List")]
public class MirkovaHra_BuildingsList : ScriptableObject
{
    public List<MirkovaHra_BuildingScriptableObject> buildings;
}