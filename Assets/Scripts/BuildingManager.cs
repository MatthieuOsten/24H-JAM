using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    
    #region SINGLETON
    public static BuildingManager Instance
    {
        get
        {
            return GameObject.FindObjectOfType<BuildingManager>();
        }
    }
    #endregion
    
    #region VARIABLE
    [SerializeField] public List<BuildingScriptable> buildingPrefabs = new List<BuildingScriptable>();
    #endregion
    
}
