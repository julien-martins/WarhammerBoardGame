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

    bool _movedInTurn;


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
        _movedInTurn = false;
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
        Debug.Log("Processing");



        if (ButtonsList[14].GameObject().activeSelf)
        {
            Debug.Log("Staying in the same Phase");


            switch (_turnHandeler.phase)
            {


                case Phase.Control:
                    Debug.Log("Auto controlling Focus");

                    foreach(Warjack jack in GameManager.Instance.GetActualWarcaster().warjackBattleGroup)
                {
                    if (!jack.isDisrupted && GameManager.Instance.GetActualWarcaster().actualFocus >0 && jack.actualFocus < 3)
                    {
                        jack.actualFocus += 1;
                        GameManager.Instance.GetActualWarcaster().actualFocus -= 1;
                        Debug.Log(GameManager.Instance.GetActualWarcaster().name + " 's focus :" + GameManager.Instance.GetActualWarcaster().actualFocus);
                        Debug.Log(jack.name + " 's focus :" + jack.actualFocus);

                    }
                    
                        
                }
                    
                    
                    break;


                case Phase.Activation:
                    
                    
                    
                    Debug.Log("Activating");
                    
                    //If distance is ok
                    (Unit, Unit) fighters = WhoIsAttackingWho();
                    Debug.Log("Unit 1 = " + fighters.Item1);
                    Debug.Log("Unit 2 = " + fighters.Item2);

                    if (fighters.Item1.isPlayable)
                    {
                        Debug.Log(("Fighter 1 can do things"));
                        if (previouselyPlayedUnit != null)
                            if (previouselyPlayedUnit != fighters.Item1)
                            {
                                previouselyPlayedUnit.UsedWeaponList.Clear();
                                previouselyPlayedUnit.isPlayable = false;
                                _movedInTurn = false;
                            }
                            else
                            {
                                _movedInTurn = false;

                            }


                        if (ButtonsList[8].GameObject().activeSelf)
                        {
                            Debug.Log(("Atack 1 selected"));

                            AttackOne();
                        } else if (ButtonsList[9].GameObject().activeSelf)
                        {
                            Debug.Log(("Atack 2 selected"));

                            AttackTwo();

                        } else if (ButtonsList[10].GameObject().activeSelf)
                        {
                            Debug.Log(("Atack 3 selected"));

                            AttackThree();

                        }
                        else if (!_movedInTurn)
                        {
                            Debug.Log("Moving " + fighters.Item1.name);
                            fighters.Item1.DrawDistanceCircle();
                            _movedInTurn = true;
                        }

                            

                        if (currentWeaponToCome == -1 || currentWeaponToCome > fighters.Item1.listOfWeapons.Count)
                        {
                            _turnHandeler.ErrorDuringGame("No weapon selcted");
                        }
                        else
                        {

                            currentWeapon = fighters.Item1.listOfWeapons[currentWeaponToCome];

                            if (!fighters.Item1.isWeaponUsed(currentWeapon) && fighters.Item1.isInRangeToAttack(fighters.Item2, currentWeapon.rng))
                            {
                                fightManager.Attacking(fighters.Item1, fighters.Item2, currentWeapon, ButtonsList[12].GameObject().activeSelf, ButtonsList[13].GameObject().activeSelf);
                                fighters.Item1.UsedWeaponList.Add(currentWeapon);


                            }

                            else if(fighters.Item1.isWeaponUsed(currentWeapon) && ButtonsList[13].GameObject().activeSelf && fighters.Item1.actualFocus > 0 && fighters.Item1.isInRangeToAttack(fighters.Item2, currentWeapon.rng))
                            {
                                fighters.Item1.actualFocus = fighters.Item1.actualFocus - 1;
                                fightManager.Attacking(fighters.Item1, fighters.Item2, currentWeapon, ButtonsList[12].GameObject().activeSelf, ButtonsList[13].GameObject().activeSelf);

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
        else if(ButtonsList[15].GameObject().activeSelf)
        {
            Debug.Log("Changing Phase");

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
        else
        {
            Debug.Log("No phase");

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
            currentUnitTestBottom = Bottomcaster;
        else if (ButtonsList[5].GameObject().activeSelf)
            currentUnitTestBottom = Bottomcaster.warjackBattleGroup[0];
        else if (ButtonsList[6].GameObject().activeSelf)
            currentUnitTestBottom = Bottomcaster.warjackBattleGroup[1];
        else if (ButtonsList[7].GameObject().activeSelf)
            currentUnitTestBottom = Bottomcaster.warjackBattleGroup[2];
        else
        {
            Debug.Log("Malaise");
        }
        
        
        Debug.Log(currentUnitTestTop);
        Debug.Log(currentUnitTestBottom);

        
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
