using System;
using System.Collections;
using System.Collections.Generic;
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



    [FormerlySerializedAs("_currentWeaponToCome")] public int currentWeaponToCome;


   
     private TurnsHandeler _turnHandeler;
    private void Start()
    {
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
                


            case Phase.Activation:
                Debug.Log("cum");
                break;


            default: 
                break;

        }


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
        return 1;

    }

    public void BoostHit()
    {
        isBoostingHit = true;
    }
    public void BoostDMG()
    {
        isBoostingDmg= true;

    }

    public void UseFocus()
    {
        isUsingFocus = true;
    }

    public void UnBoostHit()
    {
        isBoostingHit = false;
    }
    public void UnBoostDMG()
    {
        isBoostingDmg = false;

    }

    public void UnUseFocus()
    {
        isUsingFocus = false;
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

    public void Unit1()
    {
        currentUnit = GameManager.Instance.GetActualWarcaster();
    }
    public void Unit2()
    {
        currentUnit = GameManager.Instance.GetActualWarcaster().warjackBattleGroup[0];

    }
    public void Unit3()
    {
        currentUnit = GameManager.Instance.GetActualWarcaster().warjackBattleGroup[1];
    }
    public void Unit4()
    {
        currentUnit = GameManager.Instance.GetActualWarcaster().warjackBattleGroup[2];
    }
    public void Enemy1()
    {
        currentEnemy = GameManager.Instance.GetActualEnemyCaster();

    }
    public void Enemy2()
    {
        currentEnemy = GameManager.Instance.GetActualEnemyCaster().warjackBattleGroup[0];

    }
    public void Enemy3()
    {
        currentEnemy = GameManager.Instance.GetActualEnemyCaster().warjackBattleGroup[1];
    }
    public void Enemy4()
    {
        currentEnemy = GameManager.Instance.GetActualEnemyCaster().warjackBattleGroup[2];

    }
}
