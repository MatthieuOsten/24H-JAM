using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    
                    // Initialisation des valeurs des batiments
                    for (int i = 0; i < 8; i++)
                    {
                        _instance.BuildingValues.Add(0);
                    }
                }
            }
            return _instance;
        }
    }
    #endregion

    #region VARIABLE
    [SerializeField] private List<int> buildingValues = new List<int>();
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
            GameObject building = Instantiate(BuildingManager.Instance.buildingPrefabs[rnd].BuildingPrefabs[rnd2]);
            building.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y, 0);
        }
    }
    #endregion

    #region FUNCTION
    #endregion

}
