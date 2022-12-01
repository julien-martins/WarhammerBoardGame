using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandeler : MonoBehaviour
{
    public bool isBoostingDmg;
    public bool isBoostingHit;
    public bool isUsingFocus;
    public Unit currentUnit;
    public Unit currentEnemy;
    public Weapon currentWeapon;
    private Phase _phaseToCome;



    public int _currentWeaponToCome;


    [SerializableField]
    TurnsHandeler turnHandeler;
    private void Start()
    {
        BegginNewTurn();
    }

    private void BegginNewTurn()
    {
        _currentWeaponToCome = -1;
        isBoostingDmg = false;
        isBoostingHit = false;
        isUsingFocus = false;
        currentUnit = null;
        currentEnemy = null;
        currentWeapon = null;
        GameManager.Instance.PassTurn();
        turnHandeler.phase = Phase.Maintenance;
    }

    void ControlPhase()
    {
        _phaseToCome = Phase.Control;
    }
    void MaintenancePhase()
    {
        //New Turn
        _phaseToCome = Phase.Maintenance;

    }
    void ActivationPhase()
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



    public int countFocusUnit1() {
        return 3;
    }
    public int countFocusUnit2()
    {
        return 1;
    }
    public int countFocusUnit3()
    {
        return 1;

    }
    public int countFocusUnit4()
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
        _currentWeaponToCome = 0;
       
    }
    public void AttackTwo()
    {
        _currentWeaponToCome = 1;

       
    }
    public void AttackThree()
    {
        _currentWeaponToCome = 2;


    }

    public void Unit1()
    {
        currentUnit = GameManager.Instance.getActualWarcaster();
    }
    public void Unit2()
    {
        currentUnit = GameManager.Instance.getActualWarcaster().warjackBattleGroup[0];

    }
    public void Unit3()
    {
        currentUnit = GameManager.Instance.getActualWarcaster().warjackBattleGroup[1];
    }
    public void Unit4()
    {
        currentUnit = GameManager.Instance.getActualWarcaster().warjackBattleGroup[2];
    }
    public void Enemy1()
    {
        currentEnemy = GameManager.Instance.getActualEnemyCaster();

    }
    public void Enemy2()
    {
        currentEnemy = GameManager.Instance.getActualEnemyCaster().warjackBattleGroup[0];

    }
    public void Enemy3()
    {
        currentEnemy = GameManager.Instance.getActualEnemyCaster().warjackBattleGroup[1];
    }
    public void Enemy4()
    {
        currentEnemy = GameManager.Instance.getActualEnemyCaster().warjackBattleGroup[2];

    }
}
