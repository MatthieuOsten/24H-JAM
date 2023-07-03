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

    public enum Ending
    {
        depression,
        starvation,
        overpopulation,
        pollution
    }

    #endregion

    #region VARIABLE

    private int win = 0;
    private Ending _ending;
    [SerializeField] private string[] _nameLevels = new string[0];

    #endregion
    
    #region ACCESSEUR
    
    public int Win
    {
        get => win;
        set => win = value;
    }

    public Ending TheEnding
    {
        get => _ending;
        set => _ending = value;
    }

    #endregion

    #region FUNCTION

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void RandomLevel()
    {
        int rand = UnityEngine.Random.Range(0, _nameLevels.Length);

        if (_nameLevels != null)
        {
            if (rand < 0 && rand >= _nameLevels.Length) { rand = 0; }

            if (_nameLevels.Length > 0)
            {
                Debug.Log("Rand " + rand + " " + _nameLevels[rand]);
                LoadScene(_nameLevels[rand]);
            }
        }
    }

    #endregion

    #region UNITY FUNCTIONS

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed.");
            Application.Quit();
        }
    }

    #endregion
}
