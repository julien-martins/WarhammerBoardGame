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


    

}
