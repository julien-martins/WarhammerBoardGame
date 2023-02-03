using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

[CreateAssetMenu(fileName = "Warcaster", menuName = "Warhammer/Warcaster")]
public class Warcasters : Unit
{
    public int Focus;
    public int lifePoints;
    public List<Warjack> warjackBattleGroup;
public override bool TakesDamage(int dmg, int collumn)
    {

        if (dmg <= 0)
        {
            Debug.Log("No damages");
            return false;
        }
        else
        {

            Debug.Log(dmg + " Damages on " + name);
            lifePoints -= dmg;
            if(lifePoints <= 0)
                Debug.Log("End of the game, " + name + " Died at the hands of " + GameManager.Instance.GetActualWarcaster().name);

            return true;
        }
    }

    

}
