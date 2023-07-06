using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/character")]
public class CharacterStatues : ScriptableObject
{
    public int HP;
    public int ATK;
    public int DEF;
    public int Cost;
    public float Critical;
    public float CriticalDamage;
    public int Speed;


    [Header("Distance")]
    public float _distance = 0.3f;
}
