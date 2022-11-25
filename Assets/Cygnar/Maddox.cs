using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Maddox : Warcasters
{
    // Start is called before the first frame update
    public Maddox()
    {
        this.lifePoints = 16;
        this._spd = 6;
        this._str = 6;
        this._mat = 7;
        this._rat = 6;
        this._def = 15;
        this._arm = 16;
        this.faction = Faction.Cygnar;
        this.isOnFire = false;
        this.isKnockedDown = false;
        this.isDisrupted = false;

        this.listOfWeapons = new List<Weapon>() { new Weapon("Storm Strike", true, 10, 12, 6), new Weapon("Tempest", false, 2, 7, 6) };



    }
}
