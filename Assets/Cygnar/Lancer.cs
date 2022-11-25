using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Lanncer : Warjack
{
    // Start is called before the first frame update
    public Lanncer()
    {

        this._spd = 6;
        this._str = 9;
        this._mat = 6;
        this._rat = 6;
        this._def = 13;
        this._arm = 16;
        this.faction = Faction.Cygnar;
        dmgGrid = new DamageGrid("FireFly");
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isArcNode = true;
        this.isDisrupted = false;
        //                                      A      C      H     L     M     R      N
        this.workingParts = new List<bool>() { true, true, false, true, true, true, true };

        this.listOfWeapons = new List<Weapon>() { new Weapon("Shock Shield", false, 0.5f, 2, 3), new Weapon("War Spear", false, 2, 4, 5) };




    }
}
