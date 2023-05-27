using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BuildingScriptable", menuName = "Scriptable/BuildingScriptable", order = 1)]
public class BuildingScriptable: ScriptableObject
{
    [SerializeField] private List<TileBase> buildingPrefabs = new List<TileBase>();
    
    public List<TileBase> BuildingPrefabs
    {
        get => buildingPrefabs;
        set => buildingPrefabs = value;
    }
    
    public virtual void OnPlace(Tilemap map, Vector3Int pos) {}
}
