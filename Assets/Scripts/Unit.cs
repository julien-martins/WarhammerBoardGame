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
    public GameObject arucoGameObject;
    public Circle distanceComponent;


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
            Debug.Log("No damages");
            return false;
        }
        else
        {

            Debug.Log(dmg + " in collumn " + collumn);

            return true;
        }
    }


    public void DrawDistanceCircle()
    {
        distanceComponent.Draw(spd);
        
    }

    public void DestroyDistanceCircle()
    {
        distanceComponent.DestroyCircle();
    }


    public bool isInRangeToAttack(Unit otherUnit, float range)
    {
        return Vector3.Distance(this.arucoGameObject.transform.position, otherUnit.arucoGameObject.transform.position) < range * 500;
    }

}
