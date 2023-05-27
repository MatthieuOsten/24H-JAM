
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Hobbies", menuName = "Scriptable/Hobbies", order = 1)]
public class Hobbies : BuildingScriptable
{

    public override void OnPlace(Tilemap map, Vector3Int pos)
    {
        LevelManager levelManager = LevelManager.Instance;
        int[] tiles = levelManager.GetNeighboursTiles(map, pos);
        int cnt = 1;
        
        foreach (int tile in tiles)
        {
            if (tile == 0)
            {
                cnt--;
                levelManager.BuildingValues[tile]--;
            }
            if (tile == 2)
            {
                cnt += 2;
                levelManager.BuildingValues[tile]++;
            }
        }
        levelManager.BuildingValues[1] += cnt;
    }
    
}
