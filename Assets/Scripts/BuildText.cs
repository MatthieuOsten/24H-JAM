using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildText : MonoBehaviour
{
    #region SINGLETON
    
    private static BuildText _instance;
    
    public static BuildText Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BuildText>();
            }

            return _instance;
        }
    }

    #endregion
    
    
}
