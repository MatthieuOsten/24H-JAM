using System;
using TMPro;
using UnityEngine;

public class BuildText : MonoBehaviour
{
    #region VARIABLE
    
    private LevelManager _levelManager;
    private TextMeshProUGUI _text;
    public float time;

    #endregion
    
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
    
    #region UNITY FUNCTIONS
    
    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _text = GetComponent<TextMeshProUGUI>();
        time = 2f;
    }
    
    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    #endregion
    
    #region FUNCTIONS
    
    public void UpdateText(Vector3 pos, String value)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        _text.text = value;
        time = 2f;
    }
    
    #endregion
    
    
}
