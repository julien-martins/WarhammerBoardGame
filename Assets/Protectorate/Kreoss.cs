using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Kreoss : Warcasters
{
    // Start is called before the first frame update
    public Kreoss()
    {
        this.lifePoints = 18;
        this._spd = 5;
        this._str = 7;
        this._mat = 7;
        this._rat = 4;
        this._def = 14;
        this._arm = 16;
        this.faction = Faction.Protectorate;
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isDisrupted = false;

        this.listOfWeapons = new List<Weapon>() { new Weapon("Spellbreaker", false, 2, 7, 6) };



    }
}
