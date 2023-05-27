using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{

    #region VARIABLE
    private LevelManager _levelManager;
    private Image _image;
    #endregion

    #region UNITY FUNCTIONS

    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _image = GetComponent<Image>();
    }
    
    private void Update()
    {
        _image.sprite = _levelManager.ActualSprite;
    }
    
    #endregion

}
