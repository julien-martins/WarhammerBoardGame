using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class FireFly : Warjack
{
    // Start is called before the first frame update
    public FireFly()
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
        this.isArcNode = false;
        this.isDisrupted = false;
        //ACHLMRN
        this.workingParts = new List<bool>() { false, true, false, true, true, true, true };

        this.listOfWeapons = new List<Weapon>() { new Weapon("Storm Blaster", true, 10, 10, 3), new Weapon("Electro Glaive", false, 2, 4, 5) };




    }
}
