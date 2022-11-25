using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitData", menuName = "My game/Unit data")]
public class Unit : ScriptableObject
{

   
    public int _spd;
    public int _str;
    public int _mat;
    public int _rat;
    public int _def;
    public int _arm;
    public bool isOnFire;
    public bool isKnockedDown;
    public bool isDisrupted;
    public List<Weapon> listOfWeapons;
    public Faction faction;
    public enum Faction
    {
        Protectorate,
        Cygnar,
        Cryx
    }
}
