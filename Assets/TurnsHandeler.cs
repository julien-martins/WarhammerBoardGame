using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Phase
{
    Maintenance,
    Control,
    Activation 
}

public class TurnsHandeler 

{

    
    public Phase phase;

    private void FixedUpdate()
    {
        
    }
    
    public void firstRound()
    {
        GameManager.Instance.SetRandomCaster();
    }

    public void NewRoundStarts()
    {
        phase = Phase.Maintenance;
        foreach (Warjack jack in GameManager.Instance.GetActualWarcaster().warjackBattleGroup )
        {
            if (!jack.isDisrupted && jack.workingParts[1])
            {
                jack.actualFocus = 1;
            }
            else
            {
                jack.actualFocus = 0;
            }

            if (jack.isOnFire)
            {
                if (2 <=  Random.Range(1, 6))
                {
                    jack.isOnFire = false;
                }
            }
        }

        if (!GameManager.Instance.GetActualWarcaster().isDisrupted)
        {
            GameManager.Instance.GetActualWarcaster().actualFocus = GameManager.Instance.GetActualWarcaster().Focus;
        }
        if (GameManager.Instance.GetActualWarcaster().isOnFire)
        {
            if (2 <= Random.Range(1, 6))
            {
                GameManager.Instance.GetActualWarcaster().isOnFire = false;
            }
        }
        BegginingCommandPhase(GameManager.Instance.GetActualWarcaster());

    }
    // Start is called before the first frame update
    void Start()
    {
        firstRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BegginingCommandPhase(Warcasters currentController)
        //Check captures 
        
    {
        foreach(Warjack jack in currentController.warjackBattleGroup)
        {
            if (jack.isOnFire)
            {
                jack.TakesDamage(12 + FightManager.RollDices(false, false).Item1, Random.Range(1, 6));
            }

            jack.isDisrupted = false;
            
        }
        currentController.isDisrupted = false;

        phase = Phase.Control;
    }

    public void moveFocus(Warcasters currentController, Warjack currentWarjack)
    {
        // le joueur vient de déplacer le pion

        if (currentWarjack.isDisrupted)
            ErrorDuringGame("The Warjack is Distrupted");
        else if(currentWarjack.actualFocus >= 3)
            ErrorDuringGame("Warjacks has a maximum of 3 focus");
        else if (currentController.actualFocus == 0)
        {
            ErrorDuringGame("Warcaster has'nt enough focus!");

        }
        else if (false)
        {
            ErrorDuringGame("Warcaster is too far away from the Warjack!");

        }
        else
        {
            currentController.actualFocus -= currentController.actualFocus;
            currentWarjack.actualFocus += currentWarjack.actualFocus;
        }

        
        
    }

    public void ErrorDuringGame(string message)
    {
        Debug.Log(message);
    }
}
