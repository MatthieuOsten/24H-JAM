using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SINGLETON

    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(GameManager).ToString();
                    _instance = newObjectInstance.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    #region ENUM
    
    public enum GameState
    {
        Menu,
        Game,
        Pause,
        End
    }

    #endregion
    
    #region VARIABLE
    
    private int win = 0;
    
    #endregion
    
    #region ACCESSEUR
    
    public int Win
    {
        get => win;
        set => win = value;
    }

    #endregion

    #region FUNCTION

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    #endregion

    #region UNITY FUNCTIONS
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
        
        DontDestroyOnLoad(this);
    }

    #endregion
}
