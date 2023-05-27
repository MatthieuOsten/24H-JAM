
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Factory", menuName = "Scriptable/Factory", order = 1)]
public class Factory : BuildingScriptable
{

    public override void OnPlace(Tilemap map, Vector3Int pos)
    {
        LevelManager levelManager = LevelManager.Instance;
        int[] tiles = levelManager.GetNeighboursTiles(map, pos);
        int cnt = 1;
        
        foreach (int tile in tiles)
        {
            if (tile == 0)
                cnt++;
            if (tile == 1)
                cnt--;
            if (tile == 2)
            {
                cnt++;
                levelManager.BuildingValues[tile]--;
            }
        }
        levelManager.BuildingValues[0] += cnt;
    }
    
}
