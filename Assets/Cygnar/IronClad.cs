using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class IronClad : Warjack
{
    // Start is called before the first frame update
    public IronClad()
    {

        this._spd = 5;
        this._str = 11;
        this._mat = 7;
        this._rat = 6;
        this._def = 12;
        this._arm = 18;
        this.faction = Faction.Cygnar;
        dmgGrid = new DamageGrid("IronClad");
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isArcNode = false;
        this.isDisrupted = false;
        //                                      A      C      H     L     M     R      N
        this.workingParts = new List<bool>() { false, true, false, true, true, true, true };

        this.listOfWeapons = new List<Weapon>() { new Weapon("Quake Hammer", false, 1, 7, 3), new Weapon("Open Fist", false, 1, 3, 5) };




    }
}
