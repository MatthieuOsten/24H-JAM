using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private Camera _currentCamera;
    [SerializeField] private BuildingScriptable _allowedPlacement;
    #endregion

    #region ACCESSEUR
    public List<int> BuildingValues
    {
        get => buildingValues;
        set => buildingValues = value;
    }
    #endregion

    #region FUNCTION UNITY
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            int rnd = UnityEngine.Random.Range(0, BuildingManager.Instance.buildingPrefabs.Count);
            int rnd2 = UnityEngine.Random.Range(0, BuildingManager.Instance.buildingPrefabs[rnd].BuildingPrefabs.Count);
            Vector3 mousePos = _currentCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mousePosInt = _buildingTilemap.WorldToCell(mousePos);
            mousePosInt = new Vector3Int(
                mousePosInt.x,
                mousePosInt.y,
                Mathf.RoundToInt(_buildingTilemap.transform.position.z)
            );
            if (!_groundTilemap.GetTile(mousePosInt) || _buildingTilemap.GetTile(mousePosInt))
                return;
            int cnt = 0;
            foreach (TileBase tile in _allowedPlacement.BuildingPrefabs)
            {
                if (_groundTilemap.GetTile(mousePosInt) == tile)
                    cnt++;
            }
            if (cnt == 0)
                return;
            _buildingTilemap.SetTile(mousePosInt, 
                BuildingManager.Instance.buildingPrefabs[rnd].BuildingPrefabs[rnd2]);
        }
    }
    #endregion

    #region FUNCTION
    #endregion

}
