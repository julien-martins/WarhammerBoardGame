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



    //Test
    public bool TestCurrentPhase;
    public bool TestBoolHit;
    public bool TestBoolDmg;
    public bool TestBoolBoost;
    public Unit currentUnitTestTop;
    public Unit currentUnitTestBottom;



    [FormerlySerializedAs("_currentWeaponToCome")] public int currentWeaponToCome;



    private void Update()
    {
        //Open CV Code
        //Check AcceptButton()
    }

    private TurnsHandeler _turnHandeler;
    private void Start()
    {

        //test
        TestCurrentPhase = true;
        TestBoolHit = false;
        TestBoolDmg = false;
        TestBoolBoost = false;

        _turnHandeler = new TurnsHandeler();
        _turnHandeler.firstRound();
        BegginNewTurn();

    }

    private void BegginNewTurn()
    {
        currentWeaponToCome = -1;
        isBoostingDmg = false;
        isBoostingHit = false;
        isUsingFocus = false;
        currentUnit = null;
        currentEnemy = null;
        currentWeapon = null;
        _turnHandeler.NewRoundStarts();

    }

   /* public void ControlPhase()
    {
        _phaseToCome = Phase.Control;
    }*/
    public void MaintenancePhase()
    {
        //New Turn
        _phaseToCome = Phase.Maintenance;

    }
    public void ActivationPhase()
    {
        _phaseToCome = Phase.Control;

    }



    //Test

    public void testCurrentPhase()
    {
        TestCurrentPhase = true;
        Debug.Log("You asked for staying in the phase ");

    }
    public void  testNextPhase()
    {
        TestCurrentPhase = false;
        Debug.Log("You asked for changing in the phase ");

    }
    public void TestBoolHitButton()
    {
        TestBoolHit = !TestBoolHit;
    }
    public void TestBoolDmgButton()
    {
        TestBoolDmg = !TestBoolDmg;
    }
    public void TestBoolBoostButton()
    {
        TestBoolBoost = !TestBoolBoost;
    }
    public void TestU1T()
    {
        currentUnitTestTop = TopCaster;

    }
    public void TestU2T()
    {
        currentUnitTestTop = TopCaster.warjackBattleGroup[0];

    }
    public void TestU3T()
    {
        currentUnitTestTop = TopCaster.warjackBattleGroup[1];

    }
    public void TestU4T()
    {
        currentUnitTestTop = TopCaster.warjackBattleGroup[2];

    }
    public void TestU1B()
    {
        currentUnitTestBottom = Bottomcaster;

    }
    public void TestU2B()
    {
        currentUnitTestBottom = Bottomcaster.warjackBattleGroup[0];

    }
    public void TestU3B()
    {
        currentUnitTestBottom = Bottomcaster.warjackBattleGroup[1];

    }
    public void TestU4B()
    {
        currentUnitTestBottom = Bottomcaster.warjackBattleGroup[2];

    }

    //Test Fin


    public void AcceptButton()
    {



        if (TestCurrentPhase)
        {

            switch (_turnHandeler.phase)
            {

                case Phase.Control:
                    Debug.Log("You Can add Focus");
                    break;


                case Phase.Activation:
                    Debug.Log("Fighting");
                    //If distance is ok
                    (Unit, Unit) fighters = WhoIsAttackingWho();
                    if(currentWeaponToCome == -1 || currentWeaponToCome > fighters.Item1.listOfWeapons.Count)
                    {
                        _turnHandeler.ErrorDuringGame("No weapon selcted");
                    }
                    else
                    {

                
                    currentWeapon = fighters.Item1.listOfWeapons[currentWeaponToCome];
                    fightManager.Attacking(fighters.Item1, fighters.Item2, currentWeapon);
                    }
                    break;


                default:
                    break;

            }

        }
        else
        {
            switch (_turnHandeler.phase)
            {

                case Phase.Control:
                    Debug.Log("Going to Activation");

                    _turnHandeler.phase = Phase.Activation;
                    break;


                case Phase.Activation:
                    Debug.Log("End the roubnd!");

                    _turnHandeler.EndRound(GameManager.Instance.GetActualWarcaster());
                    break;



                default:
                    break;

            }
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
            return TopCaster.warjackBattleGroup[1];
        }
        else if (CheckifUnitIsSelectedUnit8()){
            return TopCaster.warjackBattleGroup[2];
        }
        else
        {
            _turnHandeler.ErrorDuringGame("No jack selected on one side");
            return null;
        }

    }

    private (Unit, Unit) WhoIsAttackingWho()
        //Les parenteses sont pour le test
    {
        (Unit, Unit) attackingAndDefencing;
        attackingAndDefencing.Item1 = currentUnitTestTop;

     //   attackingAndDefencing.Item1 = getUnitSelectedOnTop();
        if (GameManager.Instance.GetActualWarcaster().faction != (attackingAndDefencing.Item1.faction))
        {
            attackingAndDefencing.Item2 = attackingAndDefencing.Item1;

            //     attackingAndDefencing.Item1 = getUnitSelectedOnBottom();
            attackingAndDefencing.Item1 = currentUnitTestBottom;

        }
        else
        {

            //   attackingAndDefencing.Item2 = getUnitSelectedOnBottom();
            attackingAndDefencing.Item2 = currentUnitTestBottom;


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
