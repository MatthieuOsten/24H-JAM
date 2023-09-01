using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GroundScriptable", menuName = "Scriptable/GroundScriptable", order = 1)]
public class GroundTileScriptable : ScriptableObject
{
    [System.Serializable]
    public struct GroundTiles
    {
        private string _name;
        [SerializeField] private byte _basicTile;
        [SerializeField] private List<TileBase> _listBuildingTiles;
        [SerializeField] private bool _buildable;

        public GroundTiles(TileBase tile)
        {
            _listBuildingTiles = new List<TileBase>();

            _listBuildingTiles.Add(tile);
            _buildable = true;
            _name = tile.name;
            _basicTile = 0;
        }

        public GroundTiles(TileBase tile, bool buildable)
        {
            _listBuildingTiles = new List<TileBase>();

            _listBuildingTiles.Add(tile);
            _buildable = buildable;
            _name = tile.name;
            _basicTile = 0;
        }

        public GroundTiles(List<TileBase> tile, byte basicTile, bool buildable)
        {
            _listBuildingTiles = tile;
            _buildable = buildable;

            if (basicTile >= 0 && basicTile < _listBuildingTiles.Count)
            {
                _name = _listBuildingTiles[basicTile].name;
                _basicTile = basicTile;
            }
            else if (_listBuildingTiles.Count > 0)
            {
                _name = _listBuildingTiles[0].name;
                _basicTile = 0;
            }
            else
            {
                _name = "Error List Empty !";
                _basicTile = 0;
            }
        }

        public List<TileBase> ListBuildingTiles
        {
            get => _listBuildingTiles;
            set => _listBuildingTiles = value;
        }

        public TileBase BasicTile
        {
            get
            {
                if (_basicTile >= 0 && _basicTile < _listBuildingTiles.Count)
                {
                    return _listBuildingTiles[_basicTile];
                } 
                else if (_listBuildingTiles.Count > 0)
                {
                    return _listBuildingTiles[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Buildable
        {
            get => _buildable;
        }

        public void UpdateName()
        {
            TileBase tile = BasicTile;

            if (BasicTile != null)
            {
                _name = BasicTile.name;
            }
            else
            {
                Debug.Log("Dont have basic tile for this, name dont change : " + _name);
            }
        }

    }

    [SerializeField] private GroundTiles[] _tabGroundTiles;

    private void OnValidate()
    {
        foreach (var tile in _tabGroundTiles)
        {
            tile.UpdateName();
        }

    }

    public GroundTiles[] BuildingPrefabs
    {
        get => _tabGroundTiles;
        set => _tabGroundTiles = value;
    }

    public virtual void OnPlace(Tilemap map, Vector3Int pos) {}
}
