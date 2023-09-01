using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{

    #region SINGLETON

    /// <summary>
    ///  Force a avoir qu'un seul LevelManager
    /// </summary>
    [SerializeField] private static LevelManager _instance = null;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
                
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(LevelManager).ToString();
                    _instance = newObjectInstance.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region VARIABLE
    [SerializeField] private List<int> buildingValues = new List<int>();
    [SerializeField] private Tilemap _groundTilemap;
    [SerializeField] private Tilemap _buildingTilemap;
    [SerializeField] private Tilemap _previewTilemap;
    [SerializeField] private Camera _currentCamera;
    [SerializeField] private GroundTileScriptable _allowedPlacement;
    [SerializeField] private TileBase _actualTile;
    [SerializeField] private Sprite _actualSprite;
    [SerializeField] private int _prefabId;
    [SerializeField] private BuildText _buildText;

    [SerializeField] private Vector3Int _currentGridPosition;

    [SerializeField] private UnityEvent _placeBuilding, _newTile;

    #endregion

    #region ACCESSEUR
    public List<int> BuildingValues
    {
        get => buildingValues;
        set => buildingValues = value;
    }
    
    public Sprite ActualSprite
    {
        get => _actualSprite;
        set => _actualSprite = value;
    }

    public BuildText BuildText
    {
        get => _buildText;
        set => _buildText = value;
    }
    #endregion

    #region FUNCTION UNITY

    private void Start()
    {
        GetNewTile();
        SceneManager.LoadScene("HUD",LoadSceneMode.Additive);
    }

    private void Update()
    {
        _previewTilemap.SetTile(_currentGridPosition, null); // Sub old preview

        if (_actualTile != null)
        {
            Vector3 mousePos = _currentCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mousePosInt = _buildingTilemap.WorldToCell(mousePos);
            mousePosInt = new Vector3Int(
                mousePosInt.x,
                mousePosInt.y,
                Mathf.RoundToInt(_buildingTilemap.transform.position.z)
            );

            _currentGridPosition = mousePosInt; // Update current position

            if (CanBuild(out _))
                _previewTilemap.SetTile(mousePosInt, _actualTile); // Add new preview

            TileBase defaultTile = null;
            if (Input.GetMouseButtonUp(0) && CanBuild(out defaultTile))
            {
                _placeBuilding.Invoke();

                BuildingManager buildingManager = BuildingManager.Instance;
                _buildingTilemap.SetTile(mousePosInt, _actualTile); // Set building on map

                if (defaultTile != null) // Change the ground for building
                    _groundTilemap.SetTile(mousePosInt, defaultTile);

                Debug.Log("count " + buildingManager.buildingTiles.Count + " PrefabID " + _prefabId);
                buildingManager.buildingTiles[_prefabId].OnPlace(_buildingTilemap, mousePosInt);

                Timer.Instance.time = 5f;
                GetNewTile();

            }
        }

    }
    #endregion

    #region FUNCTION

    public bool CanBuild(Vector3Int pos)
    {

        if (!_groundTilemap.GetTile(_currentGridPosition) || _buildingTilemap.GetTile(_currentGridPosition))
        {
            return false;
        }

        for (int i = 0; i < _allowedPlacement.BuildingPrefabs.Length; i++)
        {
            if (!_allowedPlacement.BuildingPrefabs[i].Buildable) { continue; }

            foreach (TileBase tile in _allowedPlacement.BuildingPrefabs[i].ListBuildingTiles)
            {
                if (_groundTilemap.GetTile(pos) == tile)
                {
                    return true;
                }

            }
        }

        return false;

    }

    private bool CanBuild(out TileBase tileBasic)
    {
        tileBasic = null;

        if (!_groundTilemap.GetTile(_currentGridPosition) || _buildingTilemap.GetTile(_currentGridPosition))
        {
            return false;
        }

        for (int i = 0; i < _allowedPlacement.BuildingPrefabs.Length; i++)
        {
            if (!_allowedPlacement.BuildingPrefabs[i].Buildable) { continue; }

            foreach (TileBase tile in _allowedPlacement.BuildingPrefabs[i].ListBuildingTiles)
            {
                if (_groundTilemap.GetTile(_currentGridPosition) == tile)
                {
                    tileBasic = _allowedPlacement.BuildingPrefabs[i].BasicTile;
                    return true;
                }

            }
        }

        return false;

    }

    public TileBase GetNewTile()
    {
        _newTile.Invoke();

        BuildingManager manager = BuildingManager.Instance;
        int rnd = Random.Range(0, manager.buildingTiles.Count);
        int rnd2 = Random.Range(0, manager.buildingTiles[rnd].BuildingPrefabs.Count);

        _prefabId = rnd;
        _actualSprite = manager.buildingSprites[rnd].BuildingPrefabs[rnd2];
        _actualTile = manager.buildingTiles[rnd].BuildingPrefabs[rnd2];
        return _actualTile;
    }

    public int[] GetNeighboursTiles(Tilemap map, Vector3Int pos)
    {
        int[] neighbours = new int[4];
        neighbours[0] = GetTileId(map.GetTile(new Vector3Int(pos.x, pos.y + 1, pos.z)));
        neighbours[1] = GetTileId(map.GetTile(new Vector3Int(pos.x, pos.y - 1, pos.z)));
        neighbours[2] = GetTileId(map.GetTile(new Vector3Int(pos.x + 1, pos.y, pos.z)));
        neighbours[3] = GetTileId(map.GetTile(new Vector3Int(pos.x - 1, pos.y, pos.z)));
        return neighbours;
    }

    private int GetTileId(TileBase tile)
    {
        if (!tile)
            return -1;
        if (tile.name.Contains("Factory"))
            return 0;
        if (tile.name.Contains("Hobbies"))
            return 1;
        if (tile.name.Contains("House"))
            return 2;
        return -1;
    }

    public bool ValitadeConstruct(Vector3Int mousePos)
    {
        if (!_groundTilemap.GetTile(mousePos) || _buildingTilemap.GetTile(mousePos))
            return false;

        return true;
    }

    public void EndLevel(string name)
    {
        int index = -1, actualValue = 0;

        for (int i = 0; i < buildingValues.Count; i++)
        {
            if (buildingValues[i] > actualValue)
            {
                actualValue= buildingValues[i];
                index= i;
            }
        }

        if (index > -1)
        {
            if (index < (int)GameManager.Ending.pollution)
            {
                GameManager.Instance.TheEnding = (GameManager.Ending)index;
            }

            GameManager.Instance.LoadScene(name);
        }
        else
        {
            GameManager.Instance.LoadScene("MainMenu");
        }

    }

    #endregion

}
