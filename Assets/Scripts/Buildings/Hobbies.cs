
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Hobbies", menuName = "Scriptable/Hobbies", order = 1)]
public class Hobbies : BuildingTileScriptable
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
        
        Vector3 worldPos = map.CellToWorld(pos);
        worldPos.x += 0.5f;
        worldPos.y += 0.5f;
        worldPos.z = 1f;
        BuildText.Instance.UpdateText(worldPos, cnt.ToString());
    }
    
}
