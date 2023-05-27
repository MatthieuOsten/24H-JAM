using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    [SerializeField] public Tilemap _tilemap;
    [SerializeField] public Tilemap _tilemapPreview;

    [SerializeField] private TileBase _tileBase;

    [SerializeField] private Camera _currentCamera;

    [SerializeField] private Vector3 _mousePos;
    [SerializeField] private Vector3Int _mousePosInt;

    [SerializeField] private Vector3Int currentGridPosition;
    [SerializeField] private Vector3Int lastGridPosition;

    //private void Start()
    //{
    //    _tileBase = LevelManager.Instance.GetNewTile(_mousePosInt, false);
    //}

    private void Update()
    {

        //    if (_tileBase != null && _tilemap != null)
        //    {
        //        _mousePos = _currentCamera.ScreenToWorldPoint(Input.mousePosition);
        //        _mousePosInt = _tilemap.WorldToCell(_mousePos);

        //        _mousePosInt = new Vector3Int(
        //            _mousePosInt.x,
        //            _mousePosInt.y,
        //            Mathf.RoundToInt(_tilemap.transform.position.z)
        //            );

        //        if (Input.GetMouseButton(0))
        //        {

        //            if (_tilemap.GetTile(_mousePosInt) != null)
        //            {

        //                if (!LevelManager.Instance.ValitadeConstruct(_mousePosInt)) { return; }
        //                if (!LevelManager.Instance.AllowedPlacement(_mousePosInt)) { return; }

        //                _tilemap.SetTile(_mousePosInt, _tileBase);
        //                _tileBase = LevelManager.Instance.GetNewTile(_mousePosInt, true);
        //            }
        //        }
        //        else 
        //        {
        //            if (_tilemapPreview != null)
        //            {

        //                _mousePos = _currentCamera.ScreenToWorldPoint(Input.mousePosition);
        //                _mousePosInt = _tilemapPreview.WorldToCell(_mousePos);

        //                _mousePosInt = new Vector3Int(
        //                    _mousePosInt.x,
        //                    _mousePosInt.y,
        //                    Mathf.RoundToInt(_tilemapPreview.transform.position.z)
        //                    );

        //                Vector3 pos = _currentCamera.ScreenToWorldPoint(_mousePos);
        //                Vector3Int gridPos = _tilemapPreview.WorldToCell(pos);

        //                if (gridPos != currentGridPosition)
        //                {
        //                    lastGridPosition = currentGridPosition;
        //                    currentGridPosition = gridPos;

        //                    UpdatePreview();

        //                }
        //            }


        //        }
        //}

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = _currentCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mousePosInt = _tilemap.WorldToCell(mousePos);

            mousePosInt = new Vector3Int(
                mousePosInt.x,
                mousePosInt.y,
                Mathf.RoundToInt(_tilemap.transform.position.z)
            );
            if (LevelManager.Instance.ValitadeConstruct(mousePosInt))
                return;

            if (LevelManager.Instance.AllowedPlacement(mousePosInt))
                return;

            _tilemap.SetTile(mousePosInt,LevelManager.Instance.GetNewTile());
        }
        else if (_tileBase != null)
        {
            UpdatePreview();
        }

    }

    private void UpdatePreview()
    {
        // Remove old tile if existing
        _tilemapPreview.SetTile(lastGridPosition, null);
        // Set current tile to current mouse positions tile
        _tilemapPreview.SetTile(currentGridPosition, _tileBase);
    }

}
