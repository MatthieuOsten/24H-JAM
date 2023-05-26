using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingScriptable", menuName = "Scriptable/BuildingScriptable", order = 1)]
public class BuildingScriptable: ScriptableObject
{
    [SerializeField] private List<GameObject> buildingPrefabs = new List<GameObject>();
    
    public List<GameObject> BuildingPrefabs
    {
        get => buildingPrefabs;
        set => buildingPrefabs = value;
    }
}
