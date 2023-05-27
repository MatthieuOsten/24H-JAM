
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
        int income = 0;
        
        foreach (int tile in tiles)
        {
            if (tile == 0)
            {
                cnt--;
                income++;
            }
            if (tile == 1)
                cnt++;
            if (tile == 2)
                cnt++;
        }
        levelManager.BuildingValues[2] += cnt;
        levelManager.BuildingValues[0] += income;
        
        Vector3 worldPos = map.CellToWorld(pos);
        worldPos.x += 0.5f;
        worldPos.y += 0.5f;
        worldPos.z = 1f;
        BuildText.Instance.UpdateText(worldPos, "Population: " +  cnt + "\nWork: " + income);
    }
    
}
