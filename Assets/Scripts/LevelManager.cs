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
    #endregion

    #region FUNCTION
    #endregion

}