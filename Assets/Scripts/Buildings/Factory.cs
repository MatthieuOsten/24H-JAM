
public class Factory : BuildingTemplate
{

    void Start()
    {
        LevelManager.Instance.BuildingValues[Id]++;
    }
}
