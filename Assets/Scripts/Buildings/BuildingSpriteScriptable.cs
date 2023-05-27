using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BuildingSpriteScriptable", menuName = "Scriptable/BuildingSpriteScriptable", order = 1)]
public class BuildingSpriteScriptable: ScriptableObject
{
    [SerializeField] private List<Sprite> buildingPrefabs = new List<Sprite>();
    
    public List<Sprite> BuildingPrefabs
    {
        get => buildingPrefabs;
        set => buildingPrefabs = value;
    }

}
