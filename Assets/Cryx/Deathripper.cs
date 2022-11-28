using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Deathripper : Warjack
{
    // Start is called before the first frame update
    public Deathripper()
    {

        this._spd = 7;
        this._str = 7;
        this._mat = 5;
        this._rat = 5;
        this._def = 14;
        this._arm = 14;
        this.faction = Faction.Cryx;
        dmgGrid = new DamageGrid("Deathripper");
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isArcNode = true;
        this.isDisrupted = false;
        //ACHLMRN
        this.workingParts = new List<bool>() { true,true, true, false, true, false, true  };

        this.listOfWeapons = new List<Weapon>() { new Weapon("Mandibule", false, 0.5f, 5, 2)};




    }
}
