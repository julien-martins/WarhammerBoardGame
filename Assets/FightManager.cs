using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FightManager : MonoBehaviour

{

    private int _dicesToRoll;
    private bool _isCritique;

    private void Start()
    {

        _dicesToRoll = 1;
    }


  
    
    
    
    


    public void Attacking(Unit attacking, Unit attacked, Weapon weapon)
    {
        if (weapon.isRanged)
        {
            if (attacked.def <= attacking.rat +
                RollDices(attacking.workingParts[weapon.correspondingWorkingPart], IsHitBoosted(attacking)))
                //Special effects
                
                
                attacked.TakesDamage(
                    weapon.pow + RollDices(attacking.workingParts[weapon.correspondingWorkingPart],
                        IsDamageBoosted(attacking)) -
                    attacked.arm, Random.Range(1, 6));
        }
        else
        {
            if (attacked.def <= attacking.mat + RollDices(attacking.workingParts[weapon.correspondingWorkingPart],
                    IsHitBoosted(attacking)))
                //Special effects

                
                attacked.TakesDamage(
                    weapon.pow + attacking.str + RollDices(attacking.workingParts[weapon.correspondingWorkingPart],
                        IsDamageBoosted(attacking)) - attacked.arm, Random.Range(1, 6));
        }

        _isCritique = false;





    }

    private bool IsDamageBoosted(Unit attacking)
    {
        if (attacking.actualFocus > 0)
        {
            attacking.actualFocus -= 1;
            return true;



        }
        else return false;
    }

    public int RollDices(bool isCrippled, bool isBoosted)
    {
        int[] rolls = new int[3];

        _dicesToRoll = 1;
        int rollResult = 0;
        if (!isCrippled)
            _dicesToRoll += 1;
        if (isBoosted)
            _dicesToRoll += 1;

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
                        _isCritique = true;
                    }
                }
            }

        }


        return rollResult;


    }

    private bool IsHitBoosted(Unit attacking)
    {
        if (attacking.actualFocus > 0)
        {
            attacking.actualFocus -= 1;
            return true;



        }
        else return false;
    }
}
