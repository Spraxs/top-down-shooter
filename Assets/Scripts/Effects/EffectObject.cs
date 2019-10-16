using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effect Object")]
public class EffectObject : ScriptableObject
{
    public string name;
    public EffectManager.EffectType effectType;
}
