using UnityEngine;

public abstract class BuildingTemplate : MonoBehaviour
{
    
    #region VARIABLES
    [SerializeField] private int id;
    #endregion
    
    #region ACCESSEUR
    public int Id
    {
        get => id;
        set => id = value;
    }
    #endregion
}
