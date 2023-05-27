using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _splashArt;
    [SerializeField] private GameObject _camera;

    [SerializeField] private float _distance;
    [SerializeField] private float _destination;
    [SerializeField] private float _speed;

    [SerializeField] private Vector3 _acrochPoint;

    // Update is called once per frame
    void Update()
    {
        if (_camera.transform.position.x > _splashArt.transform.position.x)
        {
            _camera.transform.position = new Vector3(
            _camera.transform.position.x - Time.deltaTime * _speed,
            _camera.transform.position.y,
            _camera.transform.position.z);
        }

    }

}
