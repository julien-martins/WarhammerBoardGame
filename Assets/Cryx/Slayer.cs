using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class Slayer : Warjack
{
    // Start is called before the first frame update
    public Slayer()
    {
    
     this._spd = 6;
     this._str = 10;
     this._mat = 7;
     this._rat = 5;
     this._def = 13; 
     this._arm = 17;
     this.faction = Faction.Cryx;
     dmgGrid = new DamageGrid("Slayer");
     this.isOnFire = false;
     this.isKnockedDown = false;
     this.isArcNode = false;
     this.isDisrupted = false;
        //ACHLMRN
     this.workingParts = new List<bool>() { false, true, false, true, true, true, true };

     this.listOfWeapons = new List<Weapon>() { new Weapon("Death Claw", false, 1, 6, 3), new Weapon("Death Claw", false, 1, 6, 5), new Weapon("Tusks", false, 1, 2, 6) };




}
}
