using System;
using TMPro;
using UnityEngine;

public class BuildText : MonoBehaviour
{
    #region VARIABLE
    
    private TextMeshProUGUI _text;
    public float time;

    #endregion

    #region UNITY FUNCTIONS

    private void Awake()
    {
        if (LevelManager.Instance.BuildText == null)
        {
            LevelManager.Instance.BuildText = this;
        }
    }

    private void Start()
    {
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
