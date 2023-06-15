using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndingScriptable", menuName = "Scriptable/EndingScriptable", order = 1)]
public class ScriptableEnding : ScriptableObject
{
    [SerializeField] private List<Sprite> _listSprites;

    public List<Sprite> listSprites { get { return _listSprites; } }
}
