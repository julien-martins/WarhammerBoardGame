using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitData", menuName = "My game/Unit data")]
public class Unit : ScriptableObject
{

   
    public int spd;
    public int str;
    public int mat;
    public int rat;
    public int def;
    public int arm;
    public bool isOnFire;
    public bool isKnockedDown;
    public bool isPlayable;
    public bool isDisrupted;
    public List<Weapon> listOfWeapons;
    public Faction faction;
    public List<bool> workingParts;
    public int actualFocus;

    public List<Weapon>  UsedWeaponList ;


    public void addToUsedList(Weapon usedWeapon)
    {
        UsedWeaponList.Add(usedWeapon);
    }

    public bool isWeaponUsed(Weapon usingWeapon)
    {
        return (UsedWeaponList.Contains(usingWeapon));
    }
    public void ResetWeapons()
    {
        UsedWeaponList.Clear();
    }


    public enum Faction
    {
        Protectorate,
        Cygnar,
        Cryx
    }

    public bool TakesDamage(int dmg, int collumn)
    {
        if (dmg <= 0)
        {
            return false;
        }
        else
        {
            
            
            return true;
        }
    }
}
