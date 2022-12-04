using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ButtonHandeler : MonoBehaviour
{
    public bool isBoostingDmg;
    public bool isBoostingHit;
    public bool isUsingFocus;
    public Unit currentUnit;
    public Unit currentEnemy;
    public Weapon currentWeapon;
    private Phase _phaseToCome;
    
    [SerializableField]
    public Warcasters TopCaster;
    public Warcasters Bottomcaster;
    public FightManager fightManager;


    [FormerlySerializedAs("_currentWeaponToCome")] public int currentWeaponToCome;


   
     private TurnsHandeler _turnHandeler;
    private void Start()
    {
        BegginNewTurn();
        BegginNewTurn();
    }

    private void BegginNewTurn()
    {
        _turnHandeler = new TurnsHandeler();
        currentWeaponToCome = -1;
        isBoostingDmg = false;
        isBoostingHit = false;
        isUsingFocus = false;
        currentUnit = null;
        currentEnemy = null;
        currentWeapon = null;
        GameManager.Instance.PassTurn();
        _turnHandeler.NewRoundStarts();

    }

    public void ControlPhase()
    {
        _phaseToCome = Phase.Control;
    }
    public void MaintenancePhase()
    {
        //New Turn
        _phaseToCome = Phase.Maintenance;

    }
    public void ActivationPhase()
    {
        _phaseToCome = Phase.Control;

    }



    /*
     {
            if (_currentUnitToCome.listOfWeapons.Count >= currentWeaponToCome +1)
                currentWeaponToCome = _currentUnitToCome.listOfWeapons[currentWeaponToCome];
        }
     
     */

    public void AcceptButton()
    {
        switch (_phaseToCome)
        {
            case Phase.Maintenance:
                BegginNewTurn();
                break;


            case Phase.Control:
                break;


            case Phase.Activation:
                
                //If distance is ok
                (Unit, Unit) fighters =                 WhoIsAttackingWho();
                currentWeapon= fighters.Item1.listOfWeapons[currentWeaponToCome];
                fightManager.Attacking(fighters.Item1, fighters.Item2, currentWeapon);
                break;


            default: 
                break;

        }


    }

    private Unit getUnitSelectedOnBottom()
    {
        if (CheckifUnitIsSelectedUnit1())
        {
            return Bottomcaster;
        }
        else if (CheckifUnitIsSelectedUnit2()){
            return Bottomcaster.warjackBattleGroup[0];
        }
        else if (CheckifUnitIsSelectedUnit3()){
            return Bottomcaster.warjackBattleGroup[1];
        }
        else if (CheckifUnitIsSelectedUnit4()){
            return Bottomcaster.warjackBattleGroup[2];
        }
        else
        {
            _turnHandeler.ErrorDuringGame("No jack selected on one side");
            return null;
        }
        
    }
    private Unit getUnitSelectedOnTop()
    {
        if (CheckifUnitIsSelectedUnit5()){
            return TopCaster;

        }
        else if (CheckifUnitIsSelectedUnit6()){
            return TopCaster.warjackBattleGroup[0];
        }
        else if (CheckifUnitIsSelectedUnit7()){
            return TopCaster.warjackBattleGroup[0];
        }
        else if (CheckifUnitIsSelectedUnit8()){
            return TopCaster.warjackBattleGroup[0];
        }
        else
        {
            _turnHandeler.ErrorDuringGame("No jack selected on one side");
            return null;
        }

    }

    private (Unit, Unit) WhoIsAttackingWho()
    {
        (Unit, Unit) attackingAndDefencing;

        attackingAndDefencing.Item1 = getUnitSelectedOnTop();
        if (GameManager.Instance.GetActualWarcaster().faction != (attackingAndDefencing.Item1.faction))
        {
            attackingAndDefencing.Item2 = attackingAndDefencing.Item1;
            attackingAndDefencing.Item1 = getUnitSelectedOnBottom();
        }
        else
        {
            attackingAndDefencing.Item2 = getUnitSelectedOnBottom();

        }

        return attackingAndDefencing;
    }


    public int CountFocusUnit1() {
        return 3;
    }
    public int CountFocusUnit2()
    {
        return 1;
    }
    public int CountFocusUnit3()
    {
        return 1;

    }
    public int CountFocusUnit4()
    {
        //test if the captor sees something

        return 1;

    }

    public bool BoostHit()
    {
        return true;
    }
    public bool BoostDMG()
    {
        //test if the captor sees something
        return true;

    }

    public bool UseFocus()
    {
        return true;
    }


 
    public void AttackOne()
    {
        currentWeaponToCome = 0;
       
    }
    public void AttackTwo()
    {
        currentWeaponToCome = 1;

       
    }
    public void AttackThree()
    {
        currentWeaponToCome = 2;


    }

    public bool CheckifUnitIsSelectedUnit1()
    {
        return false;
    }
    public bool CheckifUnitIsSelectedUnit2()
    {
        return false;
    }
    public bool CheckifUnitIsSelectedUnit3()
    {
        return false;
    }
    public bool CheckifUnitIsSelectedUnit4()
    {
        return true;
    }
    public bool CheckifUnitIsSelectedUnit5()
    {
        return false;
    }
    public bool CheckifUnitIsSelectedUnit6()
    {
        return true;
    }
    public bool CheckifUnitIsSelectedUnit7()
    {
        return false;
    }
    public bool CheckifUnitIsSelectedUnit8()
    {
        return false;
    }

}
