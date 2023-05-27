
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "House", menuName = "Scriptable/House", order = 1)]
public class House : BuildingTileScriptable
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
                levelManager.BuildingValues[tile]++;
            }
            if (tile == 1)
                cnt++;
            if (tile == 2)
                cnt++;
        }
        levelManager.BuildingValues[2] += cnt;
    }
    
}
