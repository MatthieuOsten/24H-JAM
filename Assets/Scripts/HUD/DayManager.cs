using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[RequireComponent(typeof(Light2D))]
public class DayManager : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private Slider _slider;
    [SerializeField] private float _time = 0;
    [SerializeField] private float _astreFade = 0;
    [SerializeField] private Vector2 _defaultLimitIntensity = new Vector2(0.5f,2.5f);
    [SerializeField] private bool _enableLight = false;
    [SerializeField] private Light2D _light;
    [SerializeField] private bool _dontLoop = true;

    [Header("Statut")]
    [SerializeField] private bool _changeAstre = false;
    [SerializeField] private bool _switchMoonToSun = true;
    [SerializeField] private List<float> _listChangePoints = new List<float>();
    [SerializeField] private float _actualPoint = -1;

    [Header("Speed")]
    [SerializeField] private float _speed = 0.2f, _speedFade = 1f;

    [Header("Event Day")]
    [SerializeField] private UnityEvent _endOfDay = new UnityEvent();

    [Header("Event Astre")]
    [SerializeField] private UnityEvent _changeForSun = new UnityEvent();
    [SerializeField] private UnityEvent _changeForMoon = new UnityEvent();

    private void OnEnable()
    {
        if (TryGetComponent<Light2D>(out _light))
        {
            _enableLight = true;
            _light.lightType = Light2D.LightType.Global;
        }
        else
        {
            _enableLight = false;
        }
    }

    private void Start()
    {
        _changeAstre = true;
    }

    private void Update()
    {
        if (_slider != null)
        {
            _slider.value = _time;
        }
        else
        {
            _slider = FindObjectOfType<Slider>();
        }

        if (_actualPoint >= 0 && _listChangePoints.Count > 0)
        {
            if (_actualPoint < _time)
            {
                _changeAstre = true;
                foreach (var item in _listChangePoints)
                {
                    if (item > _actualPoint)
                    {
                        _actualPoint = item;
                    }
                    return;
                }

                _actualPoint = -1;
            }
        }
        else
        {
            if (_listChangePoints.Count > 0)
            _actualPoint = _listChangePoints[0];
        }

        if (_enableLight)
        {
            _light.intensity = Mathf.Clamp(_astreFade * 1.5f, _defaultLimitIntensity.x, _defaultLimitIntensity.y);
        }
        else
        {
            if (TryGetComponent<Light2D>(out _light))
            {
                _enableLight = true;
                _light.lightType = Light2D.LightType.Global;
            }
            else
            {
                _enableLight = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (!_switchMoonToSun && _time >= 0.5f)
        {
            _changeAstre = true;
        }

        if (_changeAstre)
        {
            if (_switchMoonToSun)
            {
                _changeForSun.Invoke();

                if (_astreFade < 2)
                {
                    _astreFade += Time.deltaTime * _speedFade;
                }

                if (_astreFade >= 2)
                {
                    _astreFade = 2;
                    _switchMoonToSun = !_switchMoonToSun;
                    _changeAstre = false;
                }
            }
            else
            {
                if (_astreFade > 0)
                {
                    _astreFade -= Time.deltaTime * _speedFade;
                }

                if (_astreFade <= 0)
                {
                    _changeForMoon.Invoke();

                    _astreFade = 0;
                    _switchMoonToSun = !_switchMoonToSun;
                    _changeAstre = false;
                }
            }

            UpdateAstre();
        }

        if (_time < 1)
        {
            _time += Time.deltaTime * _speed;
        }
        else if (_time >= 1)
        {
            if (!_dontLoop)
                _time = 0;
            
            _endOfDay.Invoke();
        }
    }

    private void UpdateAstre()
    {
        if (_slider != null)
        {
            Image image = _slider.handleRect.GetComponent<Image>();

            Material mat = Instantiate(image.material);
            mat.SetFloat("_Scale", _astreFade);
            image.material = mat;
        }
        else
        {
            _slider = FindObjectOfType<Slider>();
        }

    }


}
