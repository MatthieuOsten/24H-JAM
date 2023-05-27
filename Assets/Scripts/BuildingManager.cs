using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    
    #region SINGLETON
    
    private static BuildingManager _instance = null;
    
    public static BuildingManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<BuildingManager>();
            return _instance;
        }
    }
    #endregion
    
    #region VARIABLE
    [SerializeField] public List<BuildingScriptable> buildingPrefabs = new List<BuildingScriptable>();
    #endregion
    
}
