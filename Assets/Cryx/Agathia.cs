using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Agathia : Warcasters
{
    // Start is called before the first frame update
    public Agathia()
    {
        this.lifePoints = 16;
        this._spd = 6;
        this._str = 6;
        this._mat = 7;
        this._rat = 4;
        this._def = 15;
        this._arm = 15;
        this.faction = Faction.Cryx;
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isDisrupted = false;

        this.listOfWeapons = new List<Weapon>() { new Weapon("Death Kiss", false, 2, 6, 6) };



    }
}
