
public class House : BuildingTemplate
{

    void Start()
    {
        LevelManager.Instance.BuildingValues[Id]++;
    }
}
