using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveIsometric : MonoBehaviour
{
    public TileBase tile;
    public float amplitude = 0.1f;
    public float speed = 1f;

    private Tilemap tilemap;
    private Vector3Int currentTilePos;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedTilePos = tilemap.WorldToCell(mouseWorldPos);

            if (tilemap.GetTile(clickedTilePos) == null)
            {
                currentTilePos = clickedTilePos;
                tilemap.SetTile(currentTilePos, tile);
                StartCoroutine(AnimateWave());
            }
        }
    }

    private IEnumerator AnimateWave()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            float yOffset = Mathf.Sin(time * Mathf.PI * 2f) * amplitude;
            tilemap.SetTransformMatrix(currentTilePos, Matrix4x4.Translate(new Vector3(0f, yOffset, 0f)));
            yield return null;
        }
        tilemap.SetTransformMatrix(currentTilePos, Matrix4x4.identity); // Réinitialise la transformation après l'animation
    }
}
