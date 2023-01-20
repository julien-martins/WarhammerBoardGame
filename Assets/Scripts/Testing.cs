using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    public  Warcasters caster1;
    public  Warcasters caster2;
    public GridReader _gridReader;
    private Warjack actualJack;

    public void StartTesting()
    {
        InitializeGameObjectLink();
        GameManager.Instance.SetCharacters(caster1, caster2);
        GameManager.Instance.SetRandomCaster();
    }


    void InitializeGameObjectLink()
    {
        caster1.arucoGameObject = GameObject.FindWithTag(caster1.name);
        caster1.distanceComponent = caster1.arucoGameObject.GetComponentInChildren<Circle>();
        
        caster2.arucoGameObject = GameObject.FindWithTag(caster2.name);
        caster2.distanceComponent = caster2.arucoGameObject.GetComponentInChildren<Circle>();

        for (int i = 0; i < caster1.warjackBattleGroup.Count; i++)
        {
            actualJack = caster1.warjackBattleGroup[i];
            actualJack.arucoGameObject = GameObject.FindWithTag(actualJack.name);
            actualJack.distanceComponent = actualJack.arucoGameObject.GetComponentInChildren<Circle>();
            actualJack.grid = _gridReader.GetGridUnits(actualJack.name);
        }
        for (int i = 0; i < caster2.warjackBattleGroup.Count; i++)
        {
            actualJack = caster2.warjackBattleGroup[i];

            actualJack.arucoGameObject = GameObject.FindWithTag(actualJack.name);
            actualJack.distanceComponent = actualJack.arucoGameObject.GetComponentInChildren<Circle>();
            actualJack.grid = _gridReader.GetGridUnits(actualJack.name);


        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
