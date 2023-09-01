
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Factory", menuName = "Scriptable/Factory", order = 1)]
public class Factory : BuildingTileScriptable
{

    public override void OnPlace(Tilemap map, Vector3Int pos)
    {
        LevelManager levelManager = LevelManager.Instance;
        int[] tiles = levelManager.GetNeighboursTiles(map, pos);
        int cnt = 1;
        int loss = 0;
        
        foreach (int tile in tiles)
        {
            if (tile == 0)
                cnt++;
            if (tile == 1)
                cnt--;
            if (tile == 2)
            {
                cnt++;
                loss++;
            }
        }
        levelManager.BuildingValues[0] += cnt;
        levelManager.BuildingValues[2] -= loss;
        
        Vector3 worldPos = map.CellToWorld(pos);
        worldPos.x += 0.5f;
        worldPos.y += 0.5f;
        worldPos.z = 1f;

        TextPlace(worldPos, "Work: " + cnt + "\nPopulation: " + -loss);
    }
    
}
