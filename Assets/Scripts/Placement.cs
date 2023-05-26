using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;

    [SerializeField] private TileBase _tileBase;

    [SerializeField] private GameObject _gameObject;

    [SerializeField] private Camera _currentCamera;

    [SerializeField] private Vector3 _mousePos;
    [SerializeField] private Vector3Int _mousePosInt;

    [SerializeField] private List<TileBase> _tiles;

    private void Update()
    {
        _mousePos = _currentCamera.ScreenToWorldPoint(Input.mousePosition);
        _mousePosInt = _tilemap.WorldToCell(_mousePos);

        _mousePosInt = new Vector3Int(
            _mousePosInt.x,
            _mousePosInt.y,
            Mathf.RoundToInt(_tilemap.transform.position.z)
            );

        if (_tilemap.GetTile(_mousePosInt) == null)
        {
            GameObject thisObject = Instantiate(_gameObject);
            thisObject.transform.position = _tilemap.CellToWorld(_mousePosInt);
        }

        _tilemap.SetTile(_mousePosInt, _tileBase);

    }
}
