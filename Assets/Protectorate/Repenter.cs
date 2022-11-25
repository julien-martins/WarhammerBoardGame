using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Repenter : Warjack
{
    // Start is called before the first frame update
    public Repenter()
    {

        this._spd = 5;
        this._str = 9;
        this._mat = 6;
        this._rat = 5;
        this._def = 12;
        this._arm = 17;
        this.faction = Faction.Protectorate;
        dmgGrid = new DamageGrid("Repenter");
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isArcNode = false;
        this.isDisrupted = false;
        //ACHLMRN
        this.workingParts = new List<bool>() { false, true, false, true, true, true, true };

        this.listOfWeapons = new List<Weapon>() { new Weapon("Flame Thrower", true, 8, 12, 3), new Weapon("War Frail", false, 1, 4, 5) };




    }
}
