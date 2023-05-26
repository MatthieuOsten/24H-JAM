
public class Hobbies : BuildingTemplate
{

    void Start()
    {
        LevelManager.Instance.BuildingValues[Id]++;
    }
}
