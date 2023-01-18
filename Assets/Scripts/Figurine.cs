using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figurine : MonoBehaviour
{
    public Unit unitData;
    public  List<Weapon> UsedWeaponList;
    public bool IsUsed;

    // Update is called once per frame
    void Start()
    {
        IsUsed = false;
        UsedWeaponList = new List<Weapon>();
    }



}
