using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandeler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isBoostingDmg;
    public bool isBoostingHit;
    public bool isUsingFocus;
    public Unit currentUnit;
    public Unit currentEnemy;
    public Weapon currentWeapon;
    private Phase _phaseToCome;


    [SerializableField]
    TurnsHandeler turnHandeler;
    private void Start()
    {
        BegginNewTurn();
    }

    private void BegginNewTurn()
    {
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
    void AcceptButton()
    {
        switch (_phaseToCome)
        {
            case Phase.Maintenance:
                BegginNewTurn();
                break;


            case Phase.Control:
                Debug.Log("cum");
                break;


            case Phase.Activation:
                Debug.Log("cum");
                break;


            default: 
                break;

        }


    }

    void BoostHit()
    {
        
    }
    void BoostDMG()
    {

    }

    void UseFocus()
    {

    }

    void AttackOne()
    {

    }
    void AttackTwo()
    {

    }
    void AttackThree()
    {

    }

    void Unit1()
    {

    }
    void Unit2()
    {

    }
    void Unit3()
    {

    }
    void Unit4()
    {

    }
    void Enemy1()
    {

    }
    void Enemy2()
    {

    }
    void Enemy3()
    {

    }
    void Enemy4()
    {

    }
}
