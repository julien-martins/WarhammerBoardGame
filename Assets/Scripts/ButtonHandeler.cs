using System;
using System.Collections;
using System.Collections.Generic;
using ArucoUnity.Objects;
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

    [SerializableField]
    public List<ArucoObject> ButtonsList;


    //Test
    public bool TestCurrentPhase;
    public bool TestBoolHit;
    public bool TestBoolDmg;
    public bool TestBoolBoost;
    public Unit currentUnitTestTop;
    public Unit currentUnitTestBottom;
    public Unit previouselyPlayedUnit;



    public bool validationDetected;
    public bool validationCoolDown;


    [FormerlySerializedAs("_currentWeaponToCome")] public int currentWeaponToCome;


    

    private void Update()
    {
        if (!validationCoolDown)
        {
            if (ButtonsList[ButtonsList.Count -1].GameObject().activeSelf)
            {
                validationCoolDown = true;
                StartCoroutine(ValidateCountDown());
                AcceptButton();

                
           }
        }
        //Check AcceptButton()
    }
    IEnumerator ValidateCountDown()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        validationCoolDown = false;
    }

    private TurnsHandeler _turnHandeler;
    private void Start()
    {
        validationDetected = false;
        validationCoolDown = false;
        TestCurrentPhase = true;
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
        previouselyPlayedUnit = null;

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



    public void AcceptButton()
    {



        if (ButtonsList[22].GameObject().activeSelf)
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
                    if (fighters.Item1.isPlayable)
                    {
                        if (previouselyPlayedUnit != null)
                            if (previouselyPlayedUnit != fighters.Item1)
                            {
                                previouselyPlayedUnit.UsedWeaponList.Clear();
                                previouselyPlayedUnit.isPlayable = false;
                            }

                        if (ButtonsList[16].GameObject().activeSelf)
                        {
                            AttackOne();
                        } else if (ButtonsList[17].GameObject().activeSelf)
                        {
                            AttackTwo();

                        } else if (ButtonsList[18].GameObject().activeSelf)
                        {
                            AttackThree();

                        } 

                            

                        if (currentWeaponToCome == -1 || currentWeaponToCome > fighters.Item1.listOfWeapons.Count)
                        {
                            _turnHandeler.ErrorDuringGame("No weapon selcted");
                        }
                        else
                        {

                            currentWeapon = fighters.Item1.listOfWeapons[currentWeaponToCome];

                            if (!fighters.Item1.isWeaponUsed(currentWeapon))
                            {
                                fightManager.Attacking(fighters.Item1, fighters.Item2, currentWeapon, ButtonsList[20].GameObject().activeSelf, ButtonsList[21].GameObject().activeSelf);
                                fighters.Item1.UsedWeaponList.Add(currentWeapon);


                            }

                            else if(fighters.Item1.isWeaponUsed(currentWeapon) && ButtonsList[21].GameObject().activeSelf && fighters.Item1.actualFocus > 0)
                            {
                                fighters.Item1.actualFocus = fighters.Item1.actualFocus - 1;
                                fightManager.Attacking(fighters.Item1, fighters.Item2, currentWeapon, ButtonsList[20].GameObject().activeSelf, ButtonsList[21].GameObject().activeSelf);

                            }
                            else
                            {
                                _turnHandeler.ErrorDuringGame("Can't attack");

                            }
                        }
                    }
                    break;


                default:
                    break;

            }

        }
        else if(ButtonsList[23].GameObject().activeSelf)
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
        if (ButtonsList[0].GameObject().activeSelf)
            currentUnitTestTop = TopCaster;
        else if (ButtonsList[1].GameObject().activeSelf)
            currentUnitTestTop = TopCaster.warjackBattleGroup[0];
        else if (ButtonsList[2].GameObject().activeSelf)
            currentUnitTestTop = TopCaster.warjackBattleGroup[1];
        else if (ButtonsList[3].GameObject().activeSelf)
            currentUnitTestTop = TopCaster.warjackBattleGroup[2];
        if (ButtonsList[4].GameObject().activeSelf)
            currentUnitTestTop = Bottomcaster;
        else if (ButtonsList[5].GameObject().activeSelf)
            currentUnitTestTop = Bottomcaster.warjackBattleGroup[0];
        else if (ButtonsList[6].GameObject().activeSelf)
            currentUnitTestTop = Bottomcaster.warjackBattleGroup[1];
        else if (ButtonsList[7].GameObject().activeSelf)
            currentUnitTestTop = Bottomcaster.warjackBattleGroup[2];
        
        
        
        
        
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
