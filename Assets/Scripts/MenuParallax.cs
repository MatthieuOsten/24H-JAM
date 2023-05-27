using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuParallax : MonoBehaviour
{
    [System.Serializable]
    private struct ElementModifier
    {
        private string _name;
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector2 _startPos;
        [SerializeField] private float _moveModifier;
        [SerializeField] private bool _artificialStartPos;

        public ElementModifier(Transform transform, float modifier)
        {
            _transform = transform;
            _startPos = (Vector2)transform.position;
            _moveModifier = modifier;
            _name = _transform.name;
            _artificialStartPos = false;
        }

        public void SetStartPos()
        {
            if (_transform != null && _artificialStartPos == false)
                _startPos = (Vector2)_transform.position;
        }

        public void SetName()
        {
            if (_transform != null)
                _name = _transform.name;

        }

        public float MoveModifier { get { return _moveModifier; } }
        public Vector2 StartPos { get { return _startPos; } }
        public Transform Transform { get { return _transform; } set { _transform = value; } }
    }

    [SerializeField] private List<ElementModifier> _modifiers = new List<ElementModifier>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in _modifiers)
        {
            item.SetStartPos();
        }   
    }

    private void OnValidate()
    {
        foreach (var item in _modifiers)
        {
            item.SetName();
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var item in _modifiers)
        {
            Debug.Log("Paralax Use");

            if (item.Transform == null) continue;

            Debug.Log("transform exist " + item.Transform.name);

            item.SetName();

            Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float posX = Mathf.Lerp(item.Transform.position.x, item.StartPos.x + (pz.x * item.MoveModifier), 2f * Time.deltaTime);
            float posY = Mathf.Lerp(item.Transform.position.y, item.StartPos.y + (pz.x * item.MoveModifier), 2f * Time.deltaTime);

            item.Transform.position = new Vector3(
                posX,
                posY,
                0
                );
        }



    }
}
