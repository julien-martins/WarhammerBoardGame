using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Phase
{
    Maintenance,
    Control,
    Activation 
}

public class TurnsHandeler : MonoBehaviour

{
    
    public Phase phase;

    
    
    public void NewRoundStarts(Warcasters currentController)
    {
        phase = Phase.Maintenance;
        foreach (Warjack jack in currentController.warjackBattleGroup )
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

        if (!currentController.isDisrupted)
        {
            currentController.actualFocus = currentController.Focus;
        }
        if (currentController.isOnFire)
        {
            if (2 <= Random.Range(1, 6))
            {
                currentController.isOnFire = false;
            }
        }
        phase = Phase.Control;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlPhase(Warcasters currentController)
        //Check captures 
        
    {
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

    private void ErrorDuringGame(string message)
    {
        Debug.Log(message);
    }
}
