using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{

    #region VARIABLE
    private LevelManager _levelManager;
    private TextMeshProUGUI _text;
    public float time;
    #endregion

    #region SINGLETON
    private static Timer _instance = null;

    public static Timer Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Timer>();
            return _instance;
        }
    }
    #endregion

    #region UNITY FUNCTIONS

    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _text = GetComponent<TextMeshProUGUI>();
        time = 5f;
    }
    
    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 5f;
            _levelManager.GetNewTile();
        }
        _text.text = time.ToString("F1") + "s";
    }

    #endregion
    
    #region FUNCTIONS
    
    #endregion

}