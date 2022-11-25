using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Crusader : Warjack
{
    // Start is called before the first frame update
    public Crusader()
    {

        this._spd = 4;
        this._str = 11;
        this._mat = 6;
        this._rat = 5;
        this._def = 10;
        this._arm = 19;
        this.faction = Faction.Protectorate;
        dmgGrid = new DamageGrid("Crusader");
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isArcNode = false;
        this.isDisrupted = false;
        //ACHLMRN
        this.workingParts = new List<bool>() { false, true, false, true, true, true, true };

        this.listOfWeapons = new List<Weapon>() { new Weapon("Open Fist", false, 1, 3, 3), new Weapon("Inferno mace", false, 1, 7, 5) };




    }
}
