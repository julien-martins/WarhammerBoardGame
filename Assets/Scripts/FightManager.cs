using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour

{

    private bool isCritique;

    private void Start()
    {

    }


  
    
    
    
    


    public void Attacking(Unit attacking, Unit attacked, Weapon weapon, bool hitBoosted, bool dmgBoosted)
    {
        Debug.Log("Dealing damages!");
        if (weapon.isRanged)
        {
            (int, bool) tupleResultRoll = RollDices(attacking.workingParts[weapon.correspondingWorkingPart-1],
            IsHitBoosted(attacking, hitBoosted));


            if (attacked.def <= attacking.rat +
                tupleResultRoll.Item1)
                Debug.Log("The weapon hit!");
                if (tupleResultRoll.Item2)
                    //Is a crit!
                    ;

            int dmgRes = ( weapon.pow + RollDices(attacking.workingParts[weapon.correspondingWorkingPart-1],
                IsDamageBoosted(attacking, dmgBoosted)).Item1);

            Debug.Log("Is hitting " + dmgRes + "Againt an armor of " + attacked.arm);

            attacked.TakesDamage(
                    dmgRes -
                    attacked.arm, Random.Range(1, 6));
        }
        else
        {
            (int, bool) tupleResultRoll = RollDices(attacking.workingParts[weapon.correspondingWorkingPart-1],
                    IsHitBoosted(attacking, hitBoosted));
            if (attacked.def <= attacking.mat + tupleResultRoll.Item1)
                Debug.Log("The weapon hit!");
            if (tupleResultRoll.Item2)
                    //is a crit!
                    ;
                    Debug.Log(weapon.correspondingWorkingPart);
            int dmgRes = ( weapon.pow+ attacking.str + RollDices(attacking.workingParts[weapon.correspondingWorkingPart -1],
                IsDamageBoosted(attacking, dmgBoosted)).Item1);

                Debug.Log("Is hitting " + dmgRes +  "Againt an armor of " + attacked.arm);
            attacked.TakesDamage(
                    dmgRes - attacked.arm, Random.Range(1, 6));

           
        }






    }

    private bool IsDamageBoosted(Unit attacking, bool dmgBoosted)
    {
        if (attacking.actualFocus > 0 && dmgBoosted)
        {
            attacking.actualFocus -= 1;
            return true;



        }
        else return false;
    }
    public static (int,bool) RollDices(bool isCrippled, bool isBoosted)
    {
        int[] rolls = new int[3];
        bool isCritique = false;
        int _dicesToRoll = 1;
        int rollResult = 0;
        if (!isCrippled)
        {
            Debug.Log("Is not crippeled");

            _dicesToRoll += 1;
        }
        if (isBoosted)
        {
            Debug.Log("Is not boosted");
            _dicesToRoll += 1;
        }
        Debug.Log("Rolling " + _dicesToRoll + " Dices");
        for (int indexDices = 0; indexDices < _dicesToRoll; indexDices++)
        {
            rolls[indexDices] = Random.Range(1, 6);
            rollResult += rolls[indexDices];
            for (int indexCrit = 0; indexCrit < indexDices; indexCrit++)
            {
                if (indexDices != indexCrit)
                {
                    if (rolls[indexDices] == rolls[indexCrit])
                    {
                        isCritique = true;
                    }
                }
            }

        }

        Debug.Log("Rolling " + rollResult);
        return (rollResult,isCritique);


    }


    private bool IsHitBoosted(Unit attacking, bool rollBoosted)
    {
        if (attacking.actualFocus > 0 && rollBoosted)
        {
            attacking.actualFocus -= 1;
            return true;



        }
        else return false;
    }
}
