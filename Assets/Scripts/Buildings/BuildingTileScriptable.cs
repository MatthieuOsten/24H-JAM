using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BuildingScriptable", menuName = "Scriptable/BuildingScriptable", order = 1)]
public class BuildingTileScriptable : ScriptableObject
{
    [SerializeField] private List<TileBase> buildingPrefabs = new List<TileBase>();

    public List<TileBase> BuildingPrefabs
    {
        get => buildingPrefabs;
        set => buildingPrefabs = value;
    }

    protected void TextPlace(Vector3 pos, string text)
    {
        if (LevelManager.Instance.BuildText != null)
        {
            LevelManager.Instance.BuildText.UpdateText(pos, text);
        }
    }

    public virtual void OnPlace(Tilemap map, Vector3Int pos) { }
}